using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApi.Logic;
using LogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> m_Logger;

    private readonly IWorkerPause m_WorkerPause;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWorkerPause workerPause)
    {
      m_Logger = logger;
      m_WorkerPause = workerPause;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      m_Logger.LogTrace("LogTrace {name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      m_Logger.LogDebug("LogDebug {name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      m_Logger.LogInformation("LogInformation {name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      m_Logger.LogWarning("LogWarning {name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      m_Logger.LogError("LogError {name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));

      //var started = _worker.Start();

      var rng = new Random();
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      })
      .ToArray();
    }

    [HttpGet("worker-enable")]
    public WorkerPauseResult StartWorker()
    {
      m_WorkerPause.UnPause();
      m_Logger.LogInformation("start-worker of {name} {method} Method Called! Result = {started}", nameof(WeatherForecastController), nameof(StartWorker), m_WorkerPause.IsPaused);
      return new WorkerPauseResult { IsPaused = m_WorkerPause.IsPaused, RequestType = PauseRequestType.UnPause };
    }


    [HttpGet("worker-disable")]
    public WorkerPauseResult StopWorker()
    {
      m_WorkerPause.Pause();
      m_Logger.LogInformation("stop-worker of {name} {method} Method Called! Result = {started}", nameof(WeatherForecastController), nameof(StopWorker), m_WorkerPause.IsPaused);
      return new WorkerPauseResult { IsPaused = m_WorkerPause.IsPaused, RequestType = PauseRequestType.Pause };
    }


    [HttpGet("worker-status")]
    public WorkerPauseResult CheckWorker()
    {
      return new WorkerPauseResult { IsPaused = m_WorkerPause.IsPaused, RequestType = PauseRequestType.Status };
    }
  }
}
