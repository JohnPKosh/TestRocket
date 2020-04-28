using Factory.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RocketConsole
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      var services = new ServiceCollection();
      ConfigureServices(services, LogLevel.Trace);
      using (ServiceProvider serviceProvider = services.BuildServiceProvider())
      {
        Worker app = serviceProvider.GetService<Worker>();
        // Start up logic here
        await app.ExecuteAsync(CancellationToken.None);
        var logger = serviceProvider.GetService<ILogger<Program>>();
        logger.LogInformation("************ Process Complete ************");
        await Task.Delay(TimeSpan.FromSeconds(5));
      }

    }

    private static void ConfigureServices(IServiceCollection services, LogLevel logLevel)
    {
      services.AddLogging(configure => configure.AddConsole())
        .AddTransient<IRocketLoader, RocketOrderFileLoader>()
        .AddTransient<Worker>();

      if (logLevel == LogLevel.Trace)
      {
        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);
      }
      else
      {
        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error);
      }
    }


  }
}
