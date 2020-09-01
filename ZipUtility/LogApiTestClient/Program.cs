using System;
using SRF.CommandItems;

namespace LogApiTestClient
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        // Call the bootstrap library method to map command line arguments to CmdAttribute concrete implementations
        Environment.ExitCode = Bootstrap.Run(args, typeof(BootstrapCommands), "The Log API test client console application");
      }
      catch (Exception e) // Safeguard for any unhandled exception, ideally we would include logging here.
      {
        Environment.ExitCode = 1;
        Console.WriteLine(e.Message);
      }
    }
  }
}
