using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace LogApi.Logic
{
  public abstract class WorkerBase : BackgroundService
  {
    #region Fields and Properties

    private readonly ILogger<WorkerBase> m_Logger;

    private readonly IBackgroundServiceToggle m_ServiceToggle;

    public WorkerBase(ILogger<WorkerBase> logger, IBackgroundServiceToggle serviceToggle)
    {
      m_Logger = logger;
      m_ServiceToggle = serviceToggle;
    }

    public int RepeatIntervalMs { get; set; } = 1000;

    public int StartUpDelayMs { get; set; } = 15_000;

    public string WorkerName { get; set; }

    #endregion

    /// <summary>
    /// This method is called when the Microsoft.Extensions.Hosting.IHostedService starts.
    /// The implementation should return a task that represents the lifetime of the long
    /// running operation(s) being performed.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      // We should allow ample time for Microsoft.Hosting.Lifetime to perform startup operations before actually running our BackgroundService.
      await Task.Delay(StartUpDelayMs, stoppingToken);

      try
      {
        while (!stoppingToken.IsCancellationRequested)
        {
          await ProcessWork(stoppingToken);
        }
      }
      catch (TaskCanceledException)
      {
        m_Logger.LogWarning("{worker} TASK CANCELLED: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
      }
      catch (Exception e)
      {
        m_Logger.LogError(e, "{worker} {emsg}: {time}", WorkerName ?? "Worker", e.Message, DateTimeOffset.Now);
      }
    }

    private async Task ProcessWork(CancellationToken stoppingToken)
    {
      try
      {
        if (!stoppingToken.IsCancellationRequested)
        {
          await Task.Delay(RepeatIntervalMs, stoppingToken);
          if (!m_ServiceToggle.IsEnabled)
          {
            m_Logger.LogTrace("{worker} DISABLED at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
          }
          else
          {
            m_Logger.LogTrace("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
            await DoWork(stoppingToken);
          }
        }
      }
      catch (TaskCanceledException)
      {
        m_Logger.LogWarning("{worker} TASK CANCELLED: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
      }
      catch (Exception e)
      {
        m_Logger.LogError(e, "{worker} {emsg}: {time}", WorkerName ?? "Worker", e.Message, DateTimeOffset.Now);
        //throw;  // TODO: Think about how to handle repeated failures
      }
    }

    public abstract Task DoWork(CancellationToken stoppingToken);
  }
}