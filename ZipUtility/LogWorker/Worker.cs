using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogWorker
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogTrace("LogTrace {name} {method} Method Called!", nameof(Worker), nameof(ExecuteAsync));
        _logger.LogDebug("LogDebug {name} {method} Method Called!", nameof(Worker), nameof(ExecuteAsync));
        _logger.LogInformation("LogInformation {name} {method} Method Called!", nameof(Worker), nameof(ExecuteAsync));
        _logger.LogWarning("LogWarning {name} {method} Method Called!", nameof(Worker), nameof(ExecuteAsync));
        _logger.LogError("LogError {name} {method} Method Called!", nameof(Worker), nameof(ExecuteAsync));



        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}
