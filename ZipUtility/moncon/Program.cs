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
using Microsoft.Extensions.Logging.Console;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Configuration;


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
      hr(ConsoleColor.DarkGray);
      con("Application Starting!", ConsoleColor.White);
      //hr(ConsoleColor.DarkGray);

      try
      {
        //con("Worker Starting...(Press CTRL + P to Pause)");
        ConsoleKeyInfo key;
        while (true)
        {
          using (CancellationTokenSource source = new CancellationTokenSource())
          {
            CancellationToken token = source.Token;
            hr(ConsoleColor.DarkGreen);
            con("Worker Starting...(Press CTRL + P to Pause)");
            hr(ConsoleColor.DarkGreen);
            logger.LogInformation("Worker Starting");
            var worker = CreateHostBuilder(args).Build().RunAsync(token);

            key = Console.ReadKey(true);
            if (((key.Modifiers & ConsoleModifiers.Control) != 0) && (key.KeyChar == '\u0010'))
            {
              logger.LogInformation("***Pausing Worker***");
              //con("Pausing Worker", ConsoleColor.DarkYellow);
              source.Cancel();
              logger.LogInformation("***Worker Paused***");
              con("Worker Paused", ConsoleColor.DarkYellow);

              bool quit;
              while (true)
              {
                con("Type \"Q\" to Quit or type \"R\" to Resume:");
                var nextStep = Console.ReadLine();
                if (nextStep.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                  con("Quiting Worker...", ConsoleColor.DarkMagenta);
                  logger.LogInformation("Quiting Worker");
                  quit = true;
                  break;
                }
                if (nextStep.Trim().Equals("R", StringComparison.OrdinalIgnoreCase))
                {
                  con("Restarting Worker...", ConsoleColor.Green);
                  logger.LogInformation("Restarting Worker");
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
        hr(ConsoleColor.Red);
        con("An Unhandled Exception Occurred: {0}\r\n{1}", ex.Message, ex.StackTrace);
        hr(ConsoleColor.Red);
        logger.LogError(ex, ex.Message);
        Environment.ExitCode = -1;
      }
      //hr(ConsoleColor.DarkGray);
      con("Goodbye!", ConsoleColor.White);
      hr(ConsoleColor.DarkGray);
      logger.LogInformation("Program Exit");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
              // config.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
              // config.AddIniFile("config.ini", optional: true, reloadOnChange: true);
            })
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
              //configureLogging.AddConsole();
              configureLogging.AddConfiguration();
              //configureLogging.AddColorConsoleLogger();

              var provider = GetLoggerProvider(LogLevel.Trace, ConsoleColor.Gray);
              configureLogging.AddProvider(provider);
              //configureLogging.SetMinimumLevel(LogLevel.Trace);

              //configureLogging.AddFilter<ConsoleLoggerProvider>("Default", LogLevel.Error);
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
    private static void con(string text, ConsoleColor color)
    {
      var orig = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.WriteLine(text);
      Console.ForegroundColor = orig;
    }

  }


}
