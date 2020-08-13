using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using SRF.FileLogging.Common;

namespace SRF.FileLogging.Structured
{
  /// <summary>
  /// Configures a StructuredLoggerOptions instance by using ConfigurationBinder.Bind against an IConfiguration.
  /// <para>This class essentially binds a StructuredLoggerOptions instance with a section in the appsettings.json file.</para>
  /// </summary>
  internal class StructuredLoggerOptionsSetup : ConfigureFromConfigurationOptions<StructuredLoggerOptions>
  {
    /// <summary>Constructor that takes the IConfiguration instance to bind against.</summary>
    public StructuredLoggerOptionsSetup(ILoggerProviderConfiguration<StructuredLoggerProvider> providerConfiguration)
        : base(providerConfiguration.Configuration) { }
  }


  /// <summary>
  /// Options for the file logger.
  /// <para>There are two ways to configure file logger: 1. using the ConfigureLogging() in Program.cs or using the appsettings.json file.</para>
  /// <para> 1. ConfigureLogging()</para>
  /// <code>
  /// .ConfigureLogging(logging =&gt;
  /// {
  ///     logging.ClearProviders();
  ///     // logging.AddStructuredLogger();
  ///     logging.AddStructuredLogger(options =&gt; {
  ///         options.MaxFileSizeInMB = 5;
  ///     });
  /// })
  /// </code>
  /// <para> 2. appsettings.json file </para>
  /// <code>
  ///   "Logging": {
  ///     "LogLevel": {
  ///       "Default": "Warning"
  ///     },
  ///     "File": {
  ///       "LogLevel": "Debug",
  ///       "MaxFileSizeInMB": 5
  ///     }
  ///   },
  /// </code>
  /// </summary>
  public class StructuredLoggerOptions : FileLoggerOptions
  {
    /// <summary>The file extension to suffix the log file with based on appsettings value or default ".log"</summary>
    public override string FileExtension { get; set; } = ".log";
  }

  /// <summary>File logger extension methods.</summary>
  static public class StructuredLoggerExtensions
  {
    /// <summary>
    /// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    /// </summary>
    static public ILoggingBuilder AddStructuredLogger(this ILoggingBuilder builder)
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
    static public ILoggingBuilder AddStructuredLogger(this ILoggingBuilder builder, Action<StructuredLoggerOptions> configure)
    {
      if (configure == null)
      {
        throw new ArgumentNullException(nameof(configure));
      }

      builder.AddStructuredLogger();
      builder.Services.Configure(configure);

      return builder;
    }
  }


}
