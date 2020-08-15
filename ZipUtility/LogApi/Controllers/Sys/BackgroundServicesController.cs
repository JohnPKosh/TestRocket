using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApi.Logic;
using LogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogApi.Controllers.Sys
{
  [Route("sys/[controller]")]
  [ApiController]
  public class BackgroundServicesController : ControllerBase
  {
    private readonly ILogger<BackgroundServicesController> m_Logger;

    private readonly IBackgroundServiceToggle m_WorkerPause;

    public BackgroundServicesController(ILogger<BackgroundServicesController> logger, IBackgroundServiceToggle workerPause)
    {
      m_Logger = logger;
      m_WorkerPause = workerPause;
    }

    // GET: api/<BackgroundServicesController>
    [HttpGet]
    public BackgroundServiceStateResult Get()
    {
      return get();
    }


    [HttpGet("enable")]
    public BackgroundServiceStateResult EnableBackgroundServices()
    {
      return enable();
    }


    [HttpGet("disable")]
    public BackgroundServiceStateResult DisableBackgroundServices()
    {
      return disable();
    }


    // POST api/<BackgroundServicesController>
    [HttpPost]
    public BackgroundServiceStateResult Post(BackgroundServiceRequest value)
    {
      switch (value.RequestType)
      {
        case BackgroundServiceSignalType.Status:
          return get();
        case BackgroundServiceSignalType.Disable:
          return disable();
        case BackgroundServiceSignalType.Enable:
          return enable();
        default:
          return get();
      }
    }




    private BackgroundServiceStateResult get()
    {
      return new BackgroundServiceStateResult { IsEnabled = m_WorkerPause.IsEnabled, RequestType = BackgroundServiceSignalType.Status };
    }

    private BackgroundServiceStateResult enable()
    {
      m_WorkerPause.Enable();
      m_Logger.LogInformation("start-worker of {name} {method} Method Called! Result = {started}", nameof(WeatherForecastController), nameof(EnableBackgroundServices), m_WorkerPause.IsEnabled);
      return new BackgroundServiceStateResult { IsEnabled = m_WorkerPause.IsEnabled, RequestType = BackgroundServiceSignalType.Enable };
    }

    private BackgroundServiceStateResult disable()
    {
      m_WorkerPause.Disable();
      m_Logger.LogInformation("stop-worker of {name} {method} Method Called! Result = {started}", nameof(WeatherForecastController), nameof(DisableBackgroundServices), m_WorkerPause.IsEnabled);
      return new BackgroundServiceStateResult { IsEnabled = m_WorkerPause.IsEnabled, RequestType = BackgroundServiceSignalType.Disable };
    }


  }
}
