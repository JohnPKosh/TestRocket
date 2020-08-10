using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ZipLib.Logging
{
  public static class ColorConsoleLoggerExtensions
  {
    public static ILoggerFactory AddColorConsoleLogger(this ILoggerFactory loggerFactory, ColorConsoleLoggerConfiguration config)
    {
      loggerFactory.AddProvider(new ColorConsoleLoggerProvider(config));
      return loggerFactory;
    }
    public static ILoggerFactory AddColorConsoleLogger(this ILoggerFactory loggerFactory)
    {
      var config = new ColorConsoleLoggerConfiguration();
      return loggerFactory.AddColorConsoleLogger(config);
    }
    public static ILoggerFactory AddColorConsoleLogger(this ILoggerFactory loggerFactory, Action<ColorConsoleLoggerConfiguration> configure)
    {
      var config = new ColorConsoleLoggerConfiguration();
      configure(config);
      return loggerFactory.AddColorConsoleLogger(config);
    }


    //public static ILoggerFactory AddColorConsoleLogger(this ILoggerFactory loggerFactory, ColorConsoleLoggerConfiguration config)
    //{
    //  loggerFactory.AddProvider(new ColorConsoleLoggerProvider(config));
    //  return loggerFactory;
    //}
    //public static ILoggerFactory AddColorConsoleLogger(this ILoggerFactory loggerFactory)
    //{
    //  var config = new ColorConsoleLoggerConfiguration();
    //  return loggerFactory.AddColorConsoleLogger(config);
    //}
    //public static ILoggerFactory AddColorConsoleLogger(this ILoggerFactory loggerFactory, Action<ColorConsoleLoggerConfiguration> configure)
    //{
    //  var config = new ColorConsoleLoggerConfiguration();
    //  configure(config);
    //  return loggerFactory.AddColorConsoleLogger(config);
    //}

    /// <summary>
    /// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    /// </summary>
    static public ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder)
    {
      builder.AddConfiguration();

      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());
      //builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<FileLoggerOptions>, FileLoggerOptionsSetup>());
      //builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<FileLoggerOptions>, LoggerProviderOptionsChangeTokenSource<FileLoggerOptions, FileLoggerProvider>>());
      return builder;
    }
    ///// <summary>
    ///// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    ///// </summary>
    //static public ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
    //{
    //  if (configure == null)
    //  {
    //    throw new ArgumentNullException(nameof(configure));
    //  }

    //  builder.AddFileLogger();
    //  builder.Services.Configure(configure);

    //  return builder;
    //}
  }
}
