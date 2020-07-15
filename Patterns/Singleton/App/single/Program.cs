using System;
using single.Models;

namespace single
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
        A Singleton represents a single instance in our application.
      */

      hr();
      con("Prepare for Launch!");
      hr();

      hr();
      con("Gathering 30 randomly distributed resources...");
      hr();

      var rb = RandomBalancer.GetBalancer();
      for (int i = 0; i < 30; i++)
      {
        var r = rb.NextResource;
        con(string.Format("Connecting to resource {0} at {1}", r.Name, r.Target));
      }

      con("Press any key to abort mission!");
      Console.ReadKey(true);
    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
  }
}
