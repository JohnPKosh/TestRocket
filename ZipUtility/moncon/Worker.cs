//#define DEBUG
//#define TRACE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ZipLib.Models.Workers;

namespace moncon
{
  public class Worker : WorkerBase
  {
    public Worker(ILogger<WorkerBase> logger) : base(logger)
    {
      WorkerName = nameof(Worker);
    }

    public override void DoWork()
    {
      Thread.Sleep(10000);
      throw new NotImplementedException();
    }
  }

  //public class Worker : BackgroundService
  //{
  //  private readonly ILogger<Worker> _logger;

  //  public Worker(ILogger<Worker> logger)
  //  {
  //    _logger = logger;
  //  }

  //  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  //  {
  //    while (!stoppingToken.IsCancellationRequested)
  //    {
  //      _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
  //      await Task.Delay(1000, stoppingToken);
  //    }
  //  }
  //}
}
