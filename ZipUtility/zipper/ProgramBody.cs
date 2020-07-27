using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using ZipLib.Enums;
using ZipLib.Ext;

namespace zipper
{
  internal static class ProgramBody
  {
    const string ROOT_DESC = "A simple compression utility console application.";

    internal static int Run(string[] args)
    {
      // Add Root Command
      var rootCommand = new RootCommand(ROOT_DESC);

      // Add Additional Commands
      rootCommand.AddCommand(GetEchoCommand());
      rootCommand.AddCommand(GetCompressCommand());
      //TODO: Add additional commands for license and Readme etc. in base class
      //TODO: General refactoring and commenting needed.

      // Invoke Command based on provided args
      return rootCommand.InvokeAsync(args).Result;
    }

    #region Echo Command Logic:

    private static Command GetEchoCommand()
    {
      var echo = new Command("echo", "The command simply ECHOs what you provide in phrase option.")
      {
         new Option<string>(
              "--phrase",
              getDefaultValue: () => "ECHO ECHO ECHO",
              description: "An option whose argument is parsed as a string")
      };
      echo.Handler = CommandHandler.Create<string>((phrase) =>
      {
        Console.WriteLine($"You specified ECHO subcommand --phrase is: {phrase}");
      });
      return echo;
    }

    #endregion

    #region Compress Command Logic:

    private static Command GetCompressCommand()
    {
      var cmd = new Command("compress");
      cmd.AddAlias("c");
      cmd.AddAlias("gzip");
      cmd.AddAlias("gz");
      cmd.Description = "The command used to GZip a file.";

      var input = new Option<FileInfo>("--input", "The input file that will be compressed.");
      input.AddAlias("-i");
      input.IsRequired = true;
      cmd.AddOption(input);

      var output = new Option<FileInfo>("--output", "The compressed output file result.");
      output.AddAlias("-o");
      output.IsRequired = true;
      cmd.AddOption(output);

      var lockwait = new Option<int>("--lockms", getDefaultValue: () => 60_000, "The compressed output file result.");
      lockwait.AddAlias("-l");
      lockwait.IsRequired = false;
      cmd.AddOption(lockwait);

      var filehandling = new Option<ExistingFileHandling>("--filehandling", getDefaultValue: () => ExistingFileHandling.Overwrite, @"PreserveExisting, Overwrite (default), ThrowException, ReplaceAndArchive");
      filehandling.AddAlias("-h");
      filehandling.IsRequired = false;
      cmd.AddOption(filehandling);

      var buffer = new Option<int>("--buffersize", getDefaultValue: () => 4096, "The buffer size in Bytes.");
      buffer.AddAlias("-b");
      buffer.IsRequired = false;
      cmd.AddOption(buffer);

      cmd.Handler = CommandHandler.Create<FileInfo, FileInfo, int, ExistingFileHandling, int>(HandleCompressCommand);
      return cmd;
    }

    private static void HandleCompressCommand(FileInfo input,
      FileInfo output,
      int lockms = 60000,
      ExistingFileHandling filehandling = ExistingFileHandling.PreserveExisting,
      int buffersize = 4096)
    {
      try
      {
        input.Compress(output, lockms, filehandling, buffersize);
        Console.WriteLine($"Compression Complete! {output.FullName}");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    #endregion

  }
}
