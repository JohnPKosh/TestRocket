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

    private readonly IBackgroundServiceToggle m_WorkerPause;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackgroundServiceToggle workerPause)
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
  }
}
