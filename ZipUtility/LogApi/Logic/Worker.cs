using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LogApi.Logic
{
  public class Worker : WorkerBase
  {
    public Worker(ILogger<WorkerBase> logger, IBackgroundServiceToggle serviceToggle) : base(logger, serviceToggle)
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
