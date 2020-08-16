using System;
using zipper.Attributes;

namespace zipper.Models
{
  public class CommandItems
  {
    /// <summary>
    /// A single string parameter test
    /// </summary>
    /// <example>
    /// zipper scream --shout "Hello World"
    /// zipper scream -s "Hello World"
    /// zipper scream \s "Hello World"
    /// </example>
    [Cmd("scream", description:"Command to execute a scream")]
    internal static void Shout([Option("--shout", "-s,\\s", description:"What you want to scream")] string shout)
    {
      Console.WriteLine(shout);
    }

    /// <summary>
    /// A single int parameter test
    /// </summary>
    /// <example>
    /// zipper count --shout 50
    /// </example>
    [Cmd("count", description: "Command to count numbers")]
    internal static void Count([Option("--shout", "-s,\\s", description: "What you want to count to...")] int shout)
    {
      Console.WriteLine(shout);
    }

    /// <summary>
    /// A dual parameter test
    /// </summary>
    /// <example>
    /// zipper duo --shout "Hello World" -n 25
    /// </example>
    [Cmd("duo", description: "Command to count numbers and shout")]
    internal static void Duo([Option("--shout", "-s,\\s", description: "What you want to scream")] string shout, [Option("--n", "-n,\\n", description: "Number to count to...")] int n)
    {
      Console.WriteLine($"{shout} {n}");
    }
  }
}
