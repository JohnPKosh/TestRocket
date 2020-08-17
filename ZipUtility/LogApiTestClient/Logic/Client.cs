using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogApiTestClient.Logic
{
  public class Client
  {
    public async Task GetAsync(string url, int n)
    {
      using var client = new HttpClient();
      for (int i = 0; i < n; i++)
      {
        var content = await client.GetStringAsync(url);
        Console.WriteLine(content);
      }
    }

    public void ParallelGet(string url, int n, int maxDop = 4)
    {
      using var client = new HttpClient();
      Parallel.For(0, n, new ParallelOptions() { MaxDegreeOfParallelism = maxDop }, i =>
      {
        var response = client.GetAsync(url).Result;
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
      });
    }
  }
}
