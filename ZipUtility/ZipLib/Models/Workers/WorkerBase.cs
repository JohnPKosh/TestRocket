﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZipLib.Models.Workers
{
  public abstract class WorkerBase : BackgroundService
  {
    private readonly ILogger<WorkerBase> _logger;

    public WorkerBase(ILogger<WorkerBase> logger)
    {
      _logger = logger;
    }

    public int RepeatIntervalMs { get; set; } = 1000;

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
        _logger.LogTrace("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
        _logger.LogDebug("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
        _logger.LogInformation("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
        _logger.LogWarning("{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
        DoWork();
        await Task.Delay(RepeatIntervalMs, stoppingToken);
      }
      catch (Exception e)
      {
        _logger.LogError(e, "{worker} running at: {time}", WorkerName ?? "Worker", DateTimeOffset.Now);
        await Task.Delay(RepeatIntervalMs, stoppingToken);
        //throw;  // TODO: Think about how to handle repeated failures
      }
    }

    public abstract void DoWork();
  }
}