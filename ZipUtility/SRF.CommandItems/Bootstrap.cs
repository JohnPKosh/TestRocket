using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using SRF.CommandItems.Attributes;

namespace SRF.CommandItems
{
  /// <summary>
  /// The static utility Bootstrap class that can be called in
  /// a console application's Program Main method to launch.
  /// </summary>
  public static class Bootstrap
  {
    /// <summary>
    /// The main method program.cs bootstrap method to execute command line arguments.
    /// </summary>
    public static int Run(string[] args, Type type, string rootDescription)
    {
      // Add Root Command
      var rootCommand = new RootCommand(rootDescription);
      // Load attributed methods from the specified type
      var cmds = CmdAttribute.GetCommands(type);
      foreach (var c in cmds)
      {
        rootCommand.AddCommand(c.GetCommand());
      }
      // Invoke Command based on provided args
      return rootCommand.InvokeAsync(args).Result;
    }
  }
}
