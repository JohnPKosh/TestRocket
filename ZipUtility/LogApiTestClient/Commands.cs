using System;
using System.Diagnostics;
using LogApiTestClient.Logic;
using SRF.CommandItems.Attributes;

namespace LogApiTestClient
{
  public class Commands
  {
    /// <summary>
    /// An HTTP GET test method attributed with CmdAttribute to be called by the main Bootstrap.Run method.
    /// </summary>
    /// <example>
    /// LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices" -n 25
    /// </example>
    [Cmd("get", description: "A command to test an API GET method.")]
    internal static void Get(
      [Option("--url", "-u,\\u", description: "The API target method URL")] string url
      ,[Option("--n", "-n,\\n", description: "Number of times to call the method")] int n
      )
    {
      Console.WriteLine($"Running {url} {n} times!");
      var client = new Client();
      var sw = new Stopwatch();
      sw.Start();
      client.GetAsync(url, n).Wait();
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// An HTTP GET test method attributed with CmdAttribute to be called by the main Bootstrap.Run method.
    /// </summary>
    /// <example>
    /// LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices" -n 25
    /// </example>
    [Cmd("pget", description: "A command to test an API GET method.")]
    internal static void ParallelGet(
      [Option("--url", "-u,\\u", description: "The API target method URL")] string url
      , [Option("--n", "-n,\\n", description: "Number of times to call the method")] int n
      , [Option("--m", "-m,\\m,-maxdop,\\maxdop", description: "Max degree of parallelism")] int maxDop = 4
      )
    {
      Console.WriteLine($"Running {url} {n} times!");
      var client = new Client();
      var sw = new Stopwatch();
      sw.Start();
      client.ParallelGet(url, n, maxDop);
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }
  }
}
