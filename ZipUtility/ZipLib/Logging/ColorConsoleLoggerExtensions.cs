using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ZipLib.Logging
{
  public static class ColorConsoleLoggerExtensions
  {
    public static ILoggerFactory AddColorConsoleLogger(
                                      this ILoggerFactory loggerFactory,
                                      ColorConsoleLoggerConfiguration config)
    {
      loggerFactory.AddProvider(new ColorConsoleLoggerProvider(config));
      return loggerFactory;
    }
    public static ILoggerFactory AddColorConsoleLogger(
                                      this ILoggerFactory loggerFactory)
    {
      var config = new ColorConsoleLoggerConfiguration();
      return loggerFactory.AddColorConsoleLogger(config);
    }
    public static ILoggerFactory AddColorConsoleLogger(
                                    this ILoggerFactory loggerFactory,
                                    Action<ColorConsoleLoggerConfiguration> configure)
    {
      var config = new ColorConsoleLoggerConfiguration();
      configure(config);
      return loggerFactory.AddColorConsoleLogger(config);
    }
  }
}
