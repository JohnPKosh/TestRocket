using System;
using System.Collections.Generic;
using System.Linq;
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
      CreateHostBuilder(args).Build().Run();
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
              configureLogging.AddProvider(GetLoggerProvider(LogLevel.Trace, ConsoleColor.Gray));
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
