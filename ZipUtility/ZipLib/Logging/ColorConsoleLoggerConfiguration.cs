using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ZipLib.Logging
{
  public class ColorConsoleLoggerConfiguration
  {
    public LogLevel LogLevel { get; set; } = LogLevel.Trace;
    public int EventId { get; set; } = 0;
    public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
    public ILogColorSettings ColorSettingss { get; set; } = LogColorSettings.Create();
}
}
