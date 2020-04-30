using System;
using System.Linq;

namespace RocketFactoryTests
{
  public static class ProgramBody
  {
    //This is a dual-mode application, if you pass `-trun` then it delegates flow to Azos `trun` utility,
    //otherwise it acts as a self-contained local web server host
    public static void Main(string[] args)
    {
      #region Run test console
      if (args.Any(a => a == "-trun"))
      {
        Azos.Tools.Trun.ProgramBody.Main(args);
        if (System.Diagnostics.Debugger.IsAttached)
        {
          Console.WriteLine("Strike any key to bail out...");
          Console.ReadKey(true);
        }
        return;
      }
      #endregion

      #region Just Run the local server
      // using (var app = new Azos.Apps.AzosApplication(args, null))
      // {
      //   using (var server = new Azos.Wave.WaveServer(app))
      //   {
      //     server.Configure(null);
      //     server.Start();
      //     Console.WriteLine("server started...");
      //     Console.WriteLine($"Use Insomnia or REST client to mess with {server.Prefixes.First()}doc/toc");
      //     Console.WriteLine("Strike <ENTER> to bail out...");
      //     Console.ReadLine();
      //   }
      // }
      #endregion
    }
  }
}
