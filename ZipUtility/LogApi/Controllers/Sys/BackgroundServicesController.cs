using LogApi.Logic;
using LogApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace LogApi.Controllers.Sys
{
  /// <summary>
  /// Represents a systems level controller that allows for API management of the BackgroundServices
  /// </summary>
  [Authorize(AuthenticationSchemes ="BasicAuthentication")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  // see https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-3.1
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
    [OpenApiOperation("GETs the background service status", "GETs and sets the current status of IsEnabled=true/false")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BackgroundServiceStateResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status401Unauthorized)]
    public BackgroundServiceStateResult Get()
    {
      return get();
    }


    [HttpGet("enable")]
    [OpenApiOperation("Signals the background service to start executing recurring tasks", "GETs and sets the current status to IsEnabled=true")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BackgroundServiceStateResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status401Unauthorized)]
    public BackgroundServiceStateResult EnableBackgroundServices()
    {
      return enable();
    }


    [HttpGet("disable")]
    [OpenApiOperation("Signals the background service to stop executing recurring tasks", "GETs and sets the current status to IsEnabled=false")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BackgroundServiceStateResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status401Unauthorized)]
    public BackgroundServiceStateResult DisableBackgroundServices()
    {
      return disable();
    }


    // POST api/<BackgroundServicesController>
    [HttpPost]
    [OpenApiOperation("POSTs a background service signal", "POSTs a BackgroundServiceRequest object to the API to Enable, Disable, or return the current Status")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BackgroundServiceStateResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
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


    // TODO: move below logic out of controller

    private BackgroundServiceStateResult get()
    {
      return new BackgroundServiceStateResult { IsEnabled = m_WorkerPause.IsEnabled, RequestType = BackgroundServiceSignalType.Status };
    }

    private BackgroundServiceStateResult enable()
    {
      m_WorkerPause.Enable();
      m_Logger.LogWarning("Enable of {name} {method} Method Called! Result = {started}", nameof(BackgroundServicesController), nameof(EnableBackgroundServices), m_WorkerPause.IsEnabled);
      return new BackgroundServiceStateResult { IsEnabled = m_WorkerPause.IsEnabled, RequestType = BackgroundServiceSignalType.Enable };
    }

    private BackgroundServiceStateResult disable()
    {
      m_WorkerPause.Disable();
      m_Logger.LogWarning("Disable of {name} {method} Method Called! Result = {started}", nameof(BackgroundServicesController), nameof(DisableBackgroundServices), m_WorkerPause.IsEnabled);
      return new BackgroundServiceStateResult { IsEnabled = m_WorkerPause.IsEnabled, RequestType = BackgroundServiceSignalType.Disable };
    }


  }
}
