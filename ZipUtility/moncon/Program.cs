//#define DEBUG
//#define TRACE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZipLib.Logging;

namespace moncon
{
  public class Program
  {

    public static void Main(string[] args)
    {
      ILoggerFactory loggerFactory = new LoggerFactory(new[] { GetLoggerProvider(LogLevel.Trace, ConsoleColor.Gray) });
      ILogger logger = loggerFactory.CreateLogger<Program>();
      logger.LogInformation("Program Start");

      try
      {
        ConsoleKeyInfo key;
        while (true)
        {
          using (CancellationTokenSource source = new CancellationTokenSource())
          {
            CancellationToken token = source.Token;
            hr(ConsoleColor.DarkGreen);
            con("Starting processes...(Press CTRL + P to Pause)");
            hr(ConsoleColor.DarkGreen);
            CreateHostBuilder(args).Build().RunAsync(token);
            key = Console.ReadKey(true);
            if (((key.Modifiers & ConsoleModifiers.Control) != 0) && (key.KeyChar == '\u0010'))
            {
              source.Cancel();
              bool quit;
              while (true)
              {
                con("Type \"Q\" to Quit or type \"R\" to Resume:");
                var nextStep = Console.ReadLine();
                if (nextStep.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                  con("Quiting...");
                  quit = true;
                  break;
                }
                if (nextStep.Trim().Equals("R", StringComparison.OrdinalIgnoreCase))
                {
                  con("Restarting...");
                  quit = false;
                  break;
                }
              }
              if (quit) break;
            }
          }
        }
      }
      catch (Exception ex)
      {
        hr();
        con("An Unhandled Exception Occurred: {0}\r\n{1}", ex.Message, ex.StackTrace);
        hr();
        logger.LogError(ex, ex.Message);
        Environment.ExitCode = -1;
      }
      hr();
      con("Goodbye!");
      hr();
      logger.LogInformation("Program Exit");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            // Configure Service
            .ConfigureServices((hostContext, services) =>
            {
              services.AddHostedService<Worker>();
            })
            // Configure Custom Logging
            .ConfigureLogging((hostBuilderContext, configureLogging) =>
            {
              configureLogging.ClearProviders(); // Clears the default Host log providers
              //DefaultTraceListener myWriter = new DefaultTraceListener();
              //Trace.Listeners.Add(myWriter);

              //configureLogging.AddDebug();


              var provider = GetLoggerProvider(LogLevel.Trace, ConsoleColor.Gray);
              configureLogging.AddProvider(provider);
              configureLogging.SetMinimumLevel(LogLevel.Trace);
            })
            ;


    private static ColorConsoleLoggerProvider GetLoggerProvider(LogLevel logLevel, ConsoleColor consoleColor)
    {
      return new ColorConsoleLoggerProvider(
                    new ColorConsoleLoggerConfiguration
                    {
                      LogLevel = logLevel,
                      Color = consoleColor
                    });
    }



    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
    private static void con(string text, params object[] args) => Console.WriteLine(text, args);

    private static void hr(ConsoleColor color)
    {
      var orig = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.WriteLine("\n**********************************\n");
      Console.ForegroundColor = orig;
    }

  }


}
