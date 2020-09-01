using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SRF.FileLogging.Structured;

namespace LogWorker
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
              logging.ClearProviders();
              logging.AddConsole();
              logging.AddSlimFileLogger();
            })
            .ConfigureServices((hostContext, services) =>
            {
              services.AddHostedService<Worker>();
            });
  }
}
