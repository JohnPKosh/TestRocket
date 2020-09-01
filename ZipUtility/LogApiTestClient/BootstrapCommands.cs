using System;
using System.Diagnostics;
using LogApiTestClient.Logic;
using SRF.BasicAuth.Models;
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
      , [Option("--user", "-user,\\user", description: "The Basic Auth UserName", required: false)] string user = null
      , [Option("--pwd", "-pwd,\\pwd", description: "The Basic Auth Password", required: false)] string pwd = null
      , [Option("--fail", "-f,\\f", description: "Max failures allowed before abandoning process", required: false)] int fail = DEFAULT_MAX_FAIL_ALLOWED
      )
    {
      if(!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pwd))
      {
        BasicAuthGet(url, ntimes, user, pwd, fail);
      }
      else
      {
        AnonymousGet(url, ntimes, fail);
      }
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
      , [Option("--user", "-user,\\user", description: "The Basic Auth UserName", required: false)] string user = null
      , [Option("--pwd", "-pwd,\\pwd", description: "The Basic Auth Password", required: false)] string pwd = null
      , [Option("--fail", "-f,\\f", description: "Max failures allowed before abandoning process", required: false)] int fail = DEFAULT_MAX_FAIL_ALLOWED
      , [Option("--maxdop", "-m,\\m", description: "Max degree of parallelism", required: false)] int maxdop = DEFAULT_MAX_DOP
      )
    {
      if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pwd))
      {
        BasicAuthParallelGet(url, ntimes, user, pwd, fail, maxdop);
      }
      else
      {
        AnonymousParallelGet(url, ntimes, fail, maxdop);
      }
    }

    [Cmd("token", description: "Gets a sample JWT Token.")]
    internal static void GetToken()
    {
      string DEFAULT_ADDRESS = "https://localhost:5001";

      var treq = new TokenRequest()
      {
        Email = "jkosh@somewhere.com",
        Name = "John Kosh",
        UserIdentity = "jkosh"
      };

      var connector = new TokenClient();

      var authenticated = connector.TryAuthenticateAsync(new Uri(DEFAULT_ADDRESS + "/token"), treq);
      var result = authenticated.Result;
      //if (!authenticated.Result) throw new Exception("fubar");
      Console.WriteLine(connector.Token);
    }

    #region Private Methods

    private static void AnonymousGet(string url, int ntimes, int fail)
    {
      Console.WriteLine($"Running {url} {ntimes} times! Max Failures Allowed: {fail}");
      var client = new Client();
      var sw = new Stopwatch();
      sw.Start();
      client.Get(url, ntimes, fail); // Pass logic to implementation
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }

    private static void BasicAuthGet(string url, int ntimes, string user, string pwd, int fail)
    {
      Console.WriteLine($"Running {url} {ntimes} times! Max Failures Allowed: {fail}");
      var client = new BasicAuthClient();
      var sw = new Stopwatch();
      sw.Start();
      client.Get(url, user, pwd, ntimes, fail); // Pass logic to implementation
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }

    private static void AnonymousParallelGet(string url, int ntimes, int fail, int maxdop)
    {
      Console.WriteLine($"Running {url} {ntimes} times! Max Failures Allowed: {fail} Max DOP: {maxdop}");
      var client = new Client();
      var sw = new Stopwatch();
      sw.Start();
      client.ParallelGet(url, ntimes, fail, maxdop); // Pass logic to implementation
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }

    private static void BasicAuthParallelGet(string url, int ntimes, string user, string pwd, int fail, int maxdop)
    {
      Console.WriteLine($"Running {url} {ntimes} times! Max Failures Allowed: {fail} Max DOP: {maxdop}");
      var client = new BasicAuthClient();
      var sw = new Stopwatch();
      sw.Start();
      client.ParallelGet(url, user, pwd, ntimes, fail, maxdop); // Pass logic to implementation
      sw.Stop();
      Console.WriteLine("Total ms run time {0}", sw.ElapsedMilliseconds);
    }

    #endregion
  }
}
