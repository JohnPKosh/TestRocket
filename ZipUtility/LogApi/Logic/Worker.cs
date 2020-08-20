using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LogApi.Logic
{
  /// <summary>
  /// Exposes a hosted background service concrete WorkerBase implementation that allows for toggling
  /// whether DoWork is enabled or not with an IBackgroundServiceToggle
  /// </summary>
  public class Worker : WorkerBase
  {
    /// <summary>
    /// The default constructor accepting an ILogger, and IBackgroundServiceToggle
    /// </summary>
    public Worker(ILogger<WorkerBase> logger, IBackgroundServiceToggle serviceToggle) : base(logger, serviceToggle)
    {
      WorkerName = nameof(Worker);
    }

    /// <summary>
    /// The method called by the underlying background service on the configure interval
    /// </summary>
    public override async Task DoWork(CancellationToken stoppingToken)
    {
      await Task.Delay(5000);
      throw new NotImplementedException();
    }
  }
}
