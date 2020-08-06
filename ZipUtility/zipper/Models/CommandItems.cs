using System;
using System.Collections.Generic;
using System.Text;
using zipper.Attributes;

namespace zipper.Models
{
  public class CommandItems
  {
    [Cmd("scream", description:"Command to execute a scream")]
    internal static void Shout([Option("--shout", "-s,\\s", description:"What you want to scream")] string shout)
    {
      Console.WriteLine(shout);
    }

    [Cmd("count", description: "Command to count numbers")]
    internal static void Count([Option("--shout", "-s,\\s", description: "What you want to count to...")] int shout)
    {
      Console.WriteLine(shout);
    }
  }
}
