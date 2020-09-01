using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogApiTestClient.Logic
{
  /// <summary>
  /// Contains simple HTTP API test runner with
  /// methods to load test endpoint methods.
  /// </summary>
  public class BasicAuthClient
  {
    private long m_SuccessCount = 0;
    private long m_FailCount = 0;

    private const int DEFAULT_MAX_FAIL_ALLOWED = 20;
    private const int DEFAULT_MAX_DOP = 4;

    /// <summary>
    /// Executes GET requests against a URL,
    /// (nTimes) number of times with optional max failures.
    /// </summary>
    public void Get(
      string url,
      string user,
      string pwd,
      int nTimes,
      int maxFailsAllowed = DEFAULT_MAX_FAIL_ALLOWED)
    {
      using var client = new HttpClient();
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        AuthenticationSchemes.Basic.ToString(),
        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{pwd}"))
        );

      for (int i = 0; i < nTimes; i++)
      {
        try
        {
          var response = client.GetAsync(url).Result;
          if (response.IsSuccessStatusCode) m_SuccessCount++;
          else m_FailCount++;
          if (m_FailCount >= maxFailsAllowed) break;
        }
        catch
        {
          m_FailCount++;
          if (m_FailCount >= maxFailsAllowed) break;
        }
      }
      Console.WriteLine($"Total requests {m_SuccessCount + m_FailCount} - {m_SuccessCount} succeeded /{m_FailCount} failed");
    }

    /// <summary>
    /// Executes Parallel GET requests against a URL,
    /// (nTimes) number of times, with a specified max DOP,
    /// and optional max failures
    /// </summary>
    public void ParallelGet(
      string url,
      string user,
      string pwd,
      int nTimes,
      int maxFailsAllowed = DEFAULT_MAX_FAIL_ALLOWED,
      int maxDop = DEFAULT_MAX_DOP)
    {
      using var client = new HttpClient();
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        AuthenticationSchemes.Basic.ToString(),
        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{pwd}"))
        );

      Parallel.For(0, nTimes, new ParallelOptions() { MaxDegreeOfParallelism = maxDop }, (i, state) =>
      {
        try
        {
          var response = client.GetAsync(url).Result;

          if (response.IsSuccessStatusCode)
            Interlocked.Increment(ref m_SuccessCount);
          else
            Interlocked.Increment(ref m_FailCount);

          if (Interlocked.Read(ref m_FailCount) >= maxFailsAllowed)
            state.Stop();
        }
        catch
        {
          Interlocked.Increment(ref m_FailCount);
          if (Interlocked.Read(ref m_FailCount) >= maxFailsAllowed) state.Stop();
        }
      });
      Console.WriteLine($"Total requests {m_SuccessCount + m_FailCount} - {m_SuccessCount} succeeded /{m_FailCount} failed");
    }
  }
}
