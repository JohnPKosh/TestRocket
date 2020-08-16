using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using SRF.CommandItems.Attributes;

namespace SRF.CommandItems
{
  public static class Bootstrap
  {
    public static int Run(string[] args, Type type, string rootDescription)
    {
      // Add Root Command
      var rootCommand = new RootCommand(rootDescription);

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
