using SRF.CommandItems;

namespace LogApiTestClient
{
  class Program
  {
    static void Main(string[] args)
    {
      // Call the bootstrap library method to map command line arguments to CmdAttribute concrete implementations
      Bootstrap.Run(args, typeof(BootstrapCommands), "The Log API test client console application");
    }
  }
}
