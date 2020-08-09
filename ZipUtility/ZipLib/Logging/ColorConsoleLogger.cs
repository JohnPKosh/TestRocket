using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ZipLib.Logging
{
  public class ColorConsoleLogger : ILogger
  {
    private readonly string _name;
    private readonly ColorConsoleLoggerConfiguration _config;

    public ColorConsoleLogger(string name, ColorConsoleLoggerConfiguration config)
    {
      _name = name;
      _config = config;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return logLevel >= _config.LogLevel;
    }

    public void Log<TState>(
      LogLevel logLevel,
      EventId eventId,
      TState state,
      Exception exception,
      Func<TState, Exception, string> formatter
      )
    {
      //////if (!IsEnabled(logLevel)) return;

      //if (logLevel == LogLevel.Debug) return;
      //if (logLevel == LogLevel.Trace) return;

      LogTextWriter.AppendLog($"{logLevel} - {eventId.Id} - {_name} - {formatter(state, exception)}");

      if (logLevel >= LogLevel.Warning) Console.WriteLine($"{logLevel} - {eventId.Id} - {_name} - {formatter(state, exception)}");

      //var color = Console.ForegroundColor;
      //Console.ForegroundColor = logLevel.LogColor(_config.ColorSettingss);
      //Console.WriteLine($"{logLevel} - {eventId.Id} - {_name} - {formatter(state, exception)}");
      //Console.ForegroundColor = color;

      ////if (_config.EventId == 0 || _config.EventId == eventId.Id)
      ////{
      //var color = Console.ForegroundColor;
      //  Console.ForegroundColor = _config.Color;
      //  Console.WriteLine($"{logLevel} - {eventId.Id} " +
      //                    $"- {_name} - {formatter(state, exception)}");
      //  Console.ForegroundColor = color;
      ////}
    }
  }
}
