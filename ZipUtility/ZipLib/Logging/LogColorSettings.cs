using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ZipLib.Logging
{
  public static class LogLevelExtensions
  {
    public static ConsoleColor LogColor(this LogLevel level, ILogColorSettings settings)
    {
      switch (level)
      {
        case LogLevel.Trace:
          return settings.TraceColor;
        case LogLevel.Debug:
          return settings.DebugColor;
        case LogLevel.Information:
          return settings.InfoColor;
        case LogLevel.Warning:
          return settings.WarnColor;
        case LogLevel.Error:
          return settings.ErrorColor;
        case LogLevel.Critical:
          return settings.CriticalColor;
        default:
          return ConsoleColor.White;
      }
    }
  }


  public interface ILogColorSettings
  {
    ConsoleColor TraceColor { get; set; }
    ConsoleColor DebugColor { get; set; }
    ConsoleColor InfoColor { get; set; }
    ConsoleColor WarnColor { get; set; }
    ConsoleColor ErrorColor { get; set; }
    ConsoleColor CriticalColor { get; set; }
  }


  public class LogColorSettings : ILogColorSettings
  {
    public static ILogColorSettings Create()
    {
      return new LogColorSettings();
    }

    public ConsoleColor TraceColor { get; set; } = ConsoleColor.Gray;
    public ConsoleColor DebugColor { get; set; } = ConsoleColor.White;
    public ConsoleColor InfoColor { get; set; } = ConsoleColor.Green;
    public ConsoleColor WarnColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;
    public ConsoleColor CriticalColor { get; set; } = ConsoleColor.Red;
  }
}
