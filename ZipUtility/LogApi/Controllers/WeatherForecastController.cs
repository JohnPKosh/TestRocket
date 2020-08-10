using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      _logger.LogWarning("{name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      //_logger.LogTrace("{name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      //_logger.LogDebug("{name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));
      _logger.Log(LogLevel.Debug, "Hello DEBUG!!!!!!");
      _logger.Log(LogLevel.Trace, "Hello TRACE!!!!!!");
      _logger.LogError("{name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));

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
