using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Factory.Logic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RocketWorker
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IRocketLoader _loader;

    public Worker(ILogger<Worker> logger, IRocketLoader loader)
    {
      _logger = logger;
      _loader = loader;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        LoadRockets();

        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
      }
    }

    private void LoadRockets()
    {
      var got = new CurrentRocketOrders(_loader);
      _logger.LogInformation("Loaded Rockets {0}", got.RocketsOrders.Any());
    }
  }
}
