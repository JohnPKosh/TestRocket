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
      //#if (DEBUG)
      //ConsoleTraceListener myWriter = new ConsoleTraceListener(false);
      //Trace.Listeners.Add(myWriter);
      //#endif

      ILoggerFactory loggerFactory = new LoggerFactory(
                            new[] { GetLoggerProvider(LogLevel.Trace, ConsoleColor.Gray) }
                        );
      //or
      //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();

      ILogger logger = loggerFactory.CreateLogger<Program>();
      logger.LogInformation("This is log message.");



      try
      {
        ConsoleKeyInfo key;
        while (true)
        {
          using (CancellationTokenSource source = new CancellationTokenSource())
          {
            CancellationToken token = source.Token;
            Console.WriteLine("Starting processes...(Press CTRL + P to Pause)");
            CreateHostBuilder(args).Build().RunAsync(token);
            key = Console.ReadKey(true);
            if (((key.Modifiers & ConsoleModifiers.Control) != 0) && (key.KeyChar == '\u0010'))
            {
              source.Cancel();
              bool quit;
              while (true)
              {
                Console.WriteLine("Type \"Q\" to Quit or type \"R\" to Resume:");
                var nextStep = Console.ReadLine();
                if (nextStep.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                  quit = true;
                  break;
                }
                if (nextStep.Trim().Equals("R", StringComparison.OrdinalIgnoreCase))
                {
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
        Console.WriteLine("An Unhandled Exception Occurred: {0}\r\n{1}", ex.Message, ex.StackTrace);
        Environment.ExitCode = -1;
      }
      Console.WriteLine("Goodbye!");
      logger.LogInformation("Goodbye!");
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

  }


}
