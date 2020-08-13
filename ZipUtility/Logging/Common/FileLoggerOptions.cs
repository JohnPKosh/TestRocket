using Microsoft.Extensions.Logging;

namespace SRF.FileLogging.Common
{
  public abstract class FileLoggerOptions
  {
    private string m_Folder;
    private int m_MaxFileSizeInMB;
    private int m_RetainPolicyFileCount;
    private int m_LogSizeCheckInterval;

    public abstract string FileExtension { get; set; }

    /// <summary>The active log level. Defaults to LogLevel.Trace</summary>
    public virtual LogLevel LogLevel { get; set; } = LogLevel.Trace;

    /// <summary>The folder where log files should be placed. Defaults to this Assembly location</summary>
    public virtual string Folder
    {
      get { return !string.IsNullOrWhiteSpace(m_Folder) ? m_Folder : System.IO.Path.GetDirectoryName(GetType().Assembly.Location); }
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

    /// <summary>Specifies after how many log entries the logger provider checks the MaxFileSizeInMb. Defaults to 100.</summary>
    public int LogSizeCheckInterval
    {
      get { return m_LogSizeCheckInterval < 100 ? 100 : m_LogSizeCheckInterval; }
      set { m_LogSizeCheckInterval = value; }
    }
  }

}
