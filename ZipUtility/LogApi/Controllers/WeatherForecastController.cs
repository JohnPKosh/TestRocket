using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

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

    /// <summary>Gets a weather forecast</summary>
    /// <response code="200" nullable="true">Weather Forecast Provided.</response>
    /// <response code="500" nullable="true">Internal Server Error.</response>
    [HttpGet]
    [OpenApiOperation("Get", "Gets weather forcasts", "Used to get a collection of weather forcasts")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), 200)]
    [ProducesResponseType(typeof(void), 500)]
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

    /// <summary>
    /// Find a Forecast
    /// </summary>
    /// <returns>WeatherForecast</returns>
    /// <response code="200" nullable="true">Weather Forecast Provided.</response>
    /// <response code="400" nullable="true">Bad Request.</response>
    /// <response code="404" nullable="true">Not Found.</response>
    [HttpGet("{summary}")]
    [OpenApiOperation("Find Summary", "Gets weather forcast by summary", "Used to get a weather forcast by summary value 0-9")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeatherForecast>> FindForecast(int summary)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        await Task.Delay(0);
        if (summary == 0)
        {
          return NotFound();
        }

        if (summary < 0 || summary > 9) summary = 0;
        return Ok(new WeatherForecast
        {
          Date = DateTime.Now,
          TemperatureC = 25,
          Summary = Summaries[summary]
        });
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
