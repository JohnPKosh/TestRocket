﻿using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;

namespace zipper
{
  class Program
  {
    static int Main(string[] args)
    {
      //return RunSub(args);
      return ProgramBody.Run(args);
    }

    private static int RunDefault(string[] args)
    {
      // Create a root command with some options
      var rootCommand = new RootCommand
      {
          new Option<int>(
              "--int-option",
              getDefaultValue: () => 42,
              description: "An option whose argument is parsed as an int"),
          new Option<bool>(
              "--bool-option",
              "An option whose argument is parsed as a bool"),
          new Option<FileInfo>(
              "--file-option",
              "An option whose argument is parsed as a FileInfo")
      };
      rootCommand.Description = "My sample app";

      // Note that the parameters of the handler method are matched according to the names of the options
      rootCommand.Handler = CommandHandler.Create<int, bool, FileInfo>((intOption, boolOption, fileOption) =>
      {
        Console.WriteLine($"The value for --int-option is: {intOption}");
        Console.WriteLine($"The value for --bool-option is: {boolOption}");
        Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
      });

      // Parse the incoming args and invoke the handler
      return rootCommand.InvokeAsync(args).Result;
    }

    private static int RunSub(string[] args)
    {
      // Create a root command with some options
      var rootCommand = new RootCommand
      {
          new Option<int>(
              "--int-option",
              getDefaultValue: () => 42,
              description: "An option whose argument is parsed as an int"),
          new Option<bool>(
              "--bool-option",
              "An option whose argument is parsed as a bool"),
          new Option<FileInfo>(
              "--file-option",
              "An option whose argument is parsed as a FileInfo")
      };
      rootCommand.Description = "My sample app";

      // Note that the parameters of the handler method are matched according to the names of the options
      rootCommand.Handler = CommandHandler.Create<int, bool, FileInfo>((intOption, boolOption, fileOption) =>
      {
        Console.WriteLine($"The value for --int-option is: {intOption}");
        Console.WriteLine($"The value for --bool-option is: {boolOption}");
        Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
      });

      var echo = new Command("echo")
      {
         new Option<string>(
              "--phrase",
              getDefaultValue: () => "ECHO ECHO ECHO",
              description: "An option whose argument is parsed as a string")
      };
      echo.Description = "The command simply ECHOs what you provide in phrase option.";
      echo.Handler = CommandHandler.Create<string>((phrase) =>
      {
        Console.WriteLine($"You specified ECHO subcommand --phrase is: {phrase}");
      });
      rootCommand.AddCommand(echo);


      // Parse the incoming args and invoke the handler
      return rootCommand.InvokeAsync(args).Result;
    }
  }
}
