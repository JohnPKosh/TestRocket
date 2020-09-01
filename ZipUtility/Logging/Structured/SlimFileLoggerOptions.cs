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
  /// Configures a SlimFileLoggerOptions instance by using ConfigurationBinder.Bind against an IConfiguration.
  /// <para>This class essentially binds a SlimFileLoggerOptions instance with a section in the appsettings.json file.</para>
  /// </summary>
  internal class SlimFileLoggerOptionsSetup : ConfigureFromConfigurationOptions<SlimFileLoggerOptions>
  {
    /// <summary>Constructor that takes the IConfiguration instance to bind against.</summary>
    public SlimFileLoggerOptionsSetup(ILoggerProviderConfiguration<SlimFileLoggerProvider> providerConfiguration)
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
  ///     // logging.AddSlimFileLogger();
  ///     logging.AddSlimFileLogger(options =&gt; {
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
  ///     "SlimFile": {
  ///       "LogLevel": {
  ///         "Default": "Trace",
  ///         "Microsoft": "Warning",
  ///         "Microsoft.Hosting.Lifetime": "Information",
  ///         "LogApi": "Trace"
  ///       },
  ///       "Folder": "./bin/Logs",
  ///       "FileExtension": ".log",
  ///       "MaxFileSizeInMB": 5,
  ///       "LogSizeCheckInterval": 200
  ///      }
  ///   },
  /// </code>
  /// </summary>
  public class SlimFileLoggerOptions : FileLoggerOptions
  {
    /// <summary>The file extension to suffix the log file with based on appsettings value or default ".log"</summary>
    public override string FileExtension { get; set; } = ".log";
  }


  /// <summary>File logger extension methods.</summary>
  static public class SlimFileLoggerExtensions
  {
    /// <summary>
    /// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    /// </summary>
    static public ILoggingBuilder AddSlimFileLogger(this ILoggingBuilder builder)
    {
      builder.AddConfiguration();

      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILogEntryWriter, SlimFileLogEntryWriter>());
      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SlimFileLoggerProvider>());
      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<SlimFileLoggerOptions>, SlimFileLoggerOptionsSetup>());
      builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<SlimFileLoggerOptions>, LoggerProviderOptionsChangeTokenSource<SlimFileLoggerOptions, SlimFileLoggerProvider>>());
      return builder;
    }

    /// <summary>
    /// Adds the file logger provider, aliased as 'File', in the available services as singleton and binds the file logger options class to the 'File' section of the appsettings.json file.
    /// </summary>
    static public ILoggingBuilder AddSlimFileLogger(this ILoggingBuilder builder, Action<SlimFileLoggerOptions> configure)
    {
      if (configure == null)
      {
        throw new ArgumentNullException(nameof(configure));
      }

      builder.AddSlimFileLogger();
      builder.Services.Configure(configure);

      return builder;
    }
  }


}
