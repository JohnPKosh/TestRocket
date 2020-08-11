﻿using Microsoft.Extensions.Logging;

namespace SRF.FileLogger
{
  /// <summary>
  /// Options for the file logger.
  /// <para>There are two ways to configure file logger: 1. using the ConfigureLogging() in Program.cs or using the appsettings.json file.</para>
  /// <para> 1. ConfigureLogging()</para>
  /// <code>
  /// .ConfigureLogging(logging =&gt;
  /// {
  ///     logging.ClearProviders();
  ///     // logging.AddFileLogger();
  ///     logging.AddFileLogger(options =&gt; {
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
  public class FileLoggerOptions
  {
    string fFolder;
    int fMaxFileSizeInMB;
    int fRetainPolicyFileCount;

    /// <summary>Constructor</summary>
    public FileLoggerOptions(){ }

    /// <summary>The active log level. Defaults to LogLevel.Information</summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Trace;

    /// <summary>The folder where log files should be placed. Defaults to this Assembly location</summary>
    public string Folder
    {
      get { return !string.IsNullOrWhiteSpace(fFolder) ? fFolder : System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location); }
      set { fFolder = value; }
    }

    /// <summary>The maximum number in MB of a single log file. Defaults to 2.</summary>
    public int MaxFileSizeInMB
    {
      get { return fMaxFileSizeInMB > 0 ? fMaxFileSizeInMB : 2; }
      set { fMaxFileSizeInMB = value; }
    }

    /// <summary>The maximum number of log files to retain. Defaults to 5.</summary>
    public int RetainPolicyFileCount
    {
      get { return fRetainPolicyFileCount < 5 ? 5 : fRetainPolicyFileCount; }
      set { fRetainPolicyFileCount = value; }
    }
  }
}
