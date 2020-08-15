using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LogApi.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SRF.FileLogging.Structured;

namespace LogApi
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

            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder
              // https://www.humankode.com/asp-net-core/develop-locally-with-https-self-signed-certificates-and-asp-net-core
              // https://github.com/dotnet/AspNetCore.Docs/blob/master/aspnetcore/fundamentals/servers/kestrel/samples/3.x/KestrelSample/Program.cs
              // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.1#kestrel-options
              .ConfigureKestrel(serverOptions =>
              {
                serverOptions.Limits.MaxConcurrentConnections = 100;
                serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                serverOptions.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
                serverOptions.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                serverOptions.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));

                serverOptions.Listen(IPAddress.Loopback, 5000);
                serverOptions.Listen(IPAddress.Loopback, 5001,
                    listenOptions =>
                    {
                      listenOptions.UseHttps("localhost.pfx", "YourSecurePassword");
                    });

                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
              }) // TODO: Move to "Kestrel" appsettings.json configuration section
              .UseStartup<Startup>();
            })

      // You can optionally add the background service in Startup.cs instead of here.
      .ConfigureServices((hostContext, services) =>
      {
        services.AddSingleton<IWorkerPause, WorkerPause>();
        services.AddHostedService<Worker>();
      })


      ;
  }
}
