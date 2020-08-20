using System;
using System.Diagnostics;
using LogApiTestClient.Logic;
using SRF.CommandItems.Attributes;

namespace LogApiTestClient
{
  /// <summary>
  /// Command line targeted methods container class to interface
  /// CmdAttribute decorated methods to specific business logic.
  /// </summary>
  public class BootstrapCommands
  {
    private const int DEFAULT_MAX_FAIL_ALLOWED = 20;
    private const int DEFAULT_MAX_DOP = 4;

    /*
      *****IMPORTANT***** - The option attribute name and parameter name must be identical including case!
      e.g. [Option("--url")] string url
    */

    /// <summary>
    /// An HTTP GET test method attributed with CmdAttribute to be called by the main Bootstrap.Run method.
    /// </summary>
    /// <example>
    /// LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices" -n 25
    /// </example>
    [Cmd("get", description: "A command to test an API GET method.")]
    internal static void Get(
      [Option("--url", "-u,\\u", description: "The API target method URL")] string url
      , [Option("--ntimes", "-n,\\n", description: "Number of times to call the API target method")] int ntimes
      , [Option("--fail", "-f,\\f", description: "Max failures allowed before abandoning process", required: false)] int fail = DEFAULT_MAX_FAIL_ALLOWED
      )
    {
      Console.WriteLine($"Running {url} {ntimes} times! Max Failures Allowed: {fail}");
      var client = new Client();
      var sw = new Stopwatch();
      sw.Start();
      client.Get(url, ntimes, fail); // Pass logic to implementation
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// An HTTP GET test method attributed with CmdAttribute to be called by the main Bootstrap.Run method.
    /// </summary>
    /// <example>
    /// LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices" -n 25
    /// </example>
    [Cmd("pget", description: "A multi-threaded parallel command to test an API GET method.")]
    internal static void ParallelGet(
      [Option("--url", "-u,\\u", description: "The API target method URL")] string url
      , [Option("--ntimes", "-n,\\n", description: "Number of times to call the API target method")] int ntimes
      , [Option("--fail", "-f,\\f", description: "Max failures allowed before abandoning process", required: false)] int fail = DEFAULT_MAX_FAIL_ALLOWED
      , [Option("--maxdop", "-m,\\m", description: "Max degree of parallelism", required: false)] int maxdop = DEFAULT_MAX_DOP
      )
    {
      Console.WriteLine($"Running {url} {ntimes} times! Max Failures Allowed: {fail}  Max DOP: {maxdop}");
      var client = new Client();
      var sw = new Stopwatch();
      sw.Start();
      client.ParallelGet(url, ntimes, maxdop, fail);  // Pass logic to implementation
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }
  }
}
