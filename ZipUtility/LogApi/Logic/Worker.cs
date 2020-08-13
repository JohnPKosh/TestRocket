using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogApi.Logic
{
  public class Worker : WorkerBase
  {
    public Worker(ILogger<WorkerBase> logger) : base(logger)
    {
      WorkerName = nameof(Worker);
    }

    public override async Task DoWork(CancellationToken stoppingToken)
    {
      await Task.Delay(5000);
      throw new NotImplementedException();
    }
  }
}
