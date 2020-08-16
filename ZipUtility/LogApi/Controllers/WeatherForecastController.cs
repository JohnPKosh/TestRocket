using System;
using System.Collections.Generic;
using System.Linq;
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

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      m_Logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      m_Logger.LogInformation("LogInformation {name} {method} Method Called!", nameof(WeatherForecastController), nameof(Get));

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
