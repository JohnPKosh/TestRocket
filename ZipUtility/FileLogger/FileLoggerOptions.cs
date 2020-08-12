using Microsoft.Extensions.Logging;

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
    private string m_Folder;
    private int m_MaxFileSizeInMB;
    private int m_RetainPolicyFileCount;

    /// <summary>The active log level. Defaults to LogLevel.Information</summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Trace;

    /// <summary>The folder where log files should be placed. Defaults to this Assembly location</summary>
    public string Folder
    {
      get { return !string.IsNullOrWhiteSpace(m_Folder) ? m_Folder : System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location); }
      set { m_Folder = value; }
    }

    /// <summary>The maximum number in MB of a single log file. Defaults to 2.</summary>
    public int MaxFileSizeInMB
    {
      get { return m_MaxFileSizeInMB > 0 ? m_MaxFileSizeInMB : 2; }
      set { m_MaxFileSizeInMB = value; }
    }

    /// <summary>The maximum number of log files to retain. Defaults to 5.</summary>
    public int RetainPolicyFileCount
    {
      get { return m_RetainPolicyFileCount < 5 ? 5 : m_RetainPolicyFileCount; }
      set { m_RetainPolicyFileCount = value; }
    }
  }
}
