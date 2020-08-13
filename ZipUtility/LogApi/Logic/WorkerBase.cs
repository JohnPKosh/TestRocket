using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace LogApi.Logic
{
  public abstract class WorkerBase : BackgroundService
  {
    private readonly ILogger<WorkerBase> _logger;

    public WorkerBase(ILogger<WorkerBase> logger)
    {
      _logger = logger;
    }

    public int RepeatIntervalMs { get; set; } = 1000;

    public int StartUpDelayMs { get; set; } = 15_000;

    public string WorkerName { get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        await ProcessWork(stoppingToken);
      }
    }

    private async Task ProcessWork(CancellationToken stoppingToken)
    {
      try
      {
        // We should allow ample time for Microsoft.Hosting.Lifetime to perform startup operations before actually running our workers.
        await Task.Delay(StartUpDelayMs, stoppingToken);

        if (!stoppingToken.IsCancellationRequested)
        {
          _logger.LogTrace("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
          _logger.LogDebug("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
          _logger.LogInformation("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
          _logger.LogWarning("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
          await DoWork(stoppingToken);
          await Task.Delay(RepeatIntervalMs, stoppingToken);
        }
      }
      catch (TaskCanceledException)
      {
        _logger.LogWarning("{worker} TASK CANCELLED: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
      }
      catch (Exception e)
      {
        _logger.LogError(e, "{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
        await Task.Delay(RepeatIntervalMs, stoppingToken);
        //throw;  // TODO: Think about how to handle repeated failures
      }
    }

    public abstract Task DoWork(CancellationToken stoppingToken);
  }
}