using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SRF.FileLogging.Structured
{
  /// <summary>File logger extension methods.</summary>
  static public class StructuredLoggerExtensions
  {
    /// <summary>
    /// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    /// </summary>
    static public ILoggingBuilder AddFileLogger(this ILoggingBuilder builder)
    {
      builder.AddConfiguration();

      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, StructuredLoggerProvider>());
      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<StructuredLoggerOptions>, StructuredLoggerOptionsSetup>());
      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<StructuredLoggerOptions>, LoggerProviderOptionsChangeTokenSource<StructuredLoggerOptions, StructuredLoggerProvider>>());
      return builder;
    }

    /// <summary>
    /// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    /// </summary>
    static public ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<StructuredLoggerOptions> configure)
    {
      if (configure == null)
      {
        throw new ArgumentNullException(nameof(configure));
      }

      builder.AddFileLogger();
      builder.Services.Configure(configure);

      return builder;
    }
  }
}
