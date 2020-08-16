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
  }
}
