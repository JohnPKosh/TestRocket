using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LogApiTestClient.Logic
{
  /// <summary>
  /// Contains simple HTTP API test runner methods to load test endpoint methods.
  /// </summary>
  public class Client
  {
    private int m_SuccessCount = 0;
    private int m_FailCount = 0;

    /// <summary>
    /// Executes GET requests against a URL, (n) number of times.
    /// </summary>
    public void Get(string url, int n)
    {
      using var client = new HttpClient();
      for (int i = 0; i < n; i++)
      {
        try
        {
          var response = client.GetAsync(url).Result;
          if (response.IsSuccessStatusCode) m_SuccessCount++;
          else m_FailCount++;
        }
        catch
        {
          m_FailCount++;
        }
      }
      Console.WriteLine($"Total requests {m_SuccessCount + m_FailCount} - {m_SuccessCount} succeeded /{m_FailCount} failed");
    }

    /// <summary>
    /// Executes Parallel GET requests against a URL, (n) number of times, with a specified max DOP.
    /// </summary>
    public void ParallelGet(string url, int n, int maxDop = 4)
    {
      using var client = new HttpClient();
      Parallel.For(0, n, new ParallelOptions() { MaxDegreeOfParallelism = maxDop }, i =>
      {
        try
        {
          var response = client.GetAsync(url).Result;
          if (response.IsSuccessStatusCode) Interlocked.Increment(ref m_SuccessCount);
          else Interlocked.Increment(ref m_FailCount); ;
        }
        catch
        {
          m_FailCount++;
        }
      });
      Console.WriteLine($"Total requests {m_SuccessCount + m_FailCount} - {m_SuccessCount} succeeded /{m_FailCount} failed");
    }
  }
}
