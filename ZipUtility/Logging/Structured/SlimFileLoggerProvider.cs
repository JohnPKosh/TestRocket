using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using SRF.FileLogging.Models;
using SRF.FileLogging.Common;

namespace SRF.FileLogging.Structured
{
  /// <summary>
  /// A logger provider that writes log entries to a text file.
  /// <para>"File" is the provider alias of this provider and can be used in the Logging section of the appsettings.json.</para>
  /// </summary>
  [ProviderAlias("SlimFile")]
  public class SlimFileLoggerProvider : LoggerProvider
  {
    #region Fields and Properties

    private bool m_Terminated;
    private string m_FilePath;
    private ConcurrentQueue<LogEntry> m_LogEntryQueue = new ConcurrentQueue<LogEntry>();

    /// <summary>The SlimFileLogEntryWriter property passed into the constructor.</summary>
    internal ILogEntryWriter LogEntryWriter { get; private set; }

    /// <summary>The SlimFileLoggerOptions property passed into the constructor.</summary>
    internal SlimFileLoggerOptions LoggerOptions { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor accepting a <![CDATA[IOptionsMonitor<SlimFileLoggerOptions>]]> that passes the current settings to the default constructor.
    /// <see cref=": https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens"/>
    /// <para>The IOptionsMonitor provides the OnChange() method which is called when the user alters the settings of this provider in the appsettings.json file.</para>
    /// </summary>
    public SlimFileLoggerProvider(IOptionsMonitor<SlimFileLoggerOptions> options, ILogEntryWriter logEntryWriter) : this(options.CurrentValue, logEntryWriter)
    {
      SettingsChangeToken = options.OnChange(changedOptions =>
      {
        LoggerOptions = changedOptions;
      });
    }

    /// <summary>The default constructor accepting a SlimFileLoggerOptions.</summary>
    public SlimFileLoggerProvider(SlimFileLoggerOptions options, ILogEntryWriter logEntryWriter)
    {
      LoggerOptions = options;
      LogEntryWriter = logEntryWriter;
      Initialize();
    }

    #endregion

    #region LoggerProvider Overrides

    protected override void Initialize()
    {
      InitializeSink(); // create the first file
      RunProcessLogTask();
    }

    /// <summary>Checks if the given logLevel is enabled. It is called by the Logger.</summary>
    public override bool IsEnabled(LogLevel logLevel) =>
          logLevel != LogLevel.None
          && LoggerOptions.LogLevel != LogLevel.None
          && Convert.ToInt32(logLevel) >= Convert.ToInt32(LoggerOptions.LogLevel);

    /// <summary>Writes the specified log information to a log file.</summary>
    public override void WriteLog(LogEntry Info) => m_LogEntryQueue.Enqueue(Info);

    /// <summary>Disposes the options change token.</summary>
    protected override void Dispose(bool disposing)
    {
      m_Terminated = true;
      base.Dispose(disposing);
    }

    #endregion

    // TODO: excise the sink specific methods below to an appropriate interface.

    #region Sink specific methods

    /// <summary>Applies the log file retains policy according to options</summary>
    private void ApplyRetainPolicy()
    {
      FileInfo FI;
      try
      {
        List<FileInfo> FileList = new DirectoryInfo(LoggerOptions.Folder)
        .GetFiles("*.log", SearchOption.TopDirectoryOnly)
        .OrderBy(fi => fi.CreationTime)
        .ToList();

        while (FileList.Count >= LoggerOptions.RetainPolicyFileCount)
        {
          FI = FileList.First();
          FI.Delete();
          FileList.Remove(FI);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message); // TODO: consider how to handle low level logging failures e.g. IOExceptions
      }
    }

    /// <summary>Creates a new disk file and writes the column titles</summary>
    private void InitializeSink()
    {
      Directory.CreateDirectory(LoggerOptions.Folder);
      m_FilePath = Path.Combine(LoggerOptions.Folder, $"{LogEntry.StaticHostName}-{DateTime.Now:yyyyMMdd-HHmm}{LoggerOptions.FileExtension}");
      WriteLogHeader();
      ApplyRetainPolicy();
    }

    /// <summary>For file based loggers that include a header row this method writes the first line to the sink</summary>
    private void WriteLogHeader()
    {
      File.WriteAllText(m_FilePath, LogEntryWriter.GenerateLogHeaderLine().ToString());
    }

    /// <summary>Dequeues all log info instances from the queue, prepares the text line, and writes to the sink.</summary>
    private void FlushLogEntryQueue()
    {
      if (m_LogEntryQueue.Count == 0) return;
      FileInfo fileinfo = new FileInfo(m_FilePath);
      if (fileinfo.Length > 1_048_576 * LoggerOptions.MaxFileSizeInMB)
      {
        InitializeSink();
        fileinfo = new FileInfo(m_FilePath);
      }
      using var fs = fileinfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
      using var sw = new StreamWriter(fs);
      while (m_LogEntryQueue.TryDequeue(out LogEntry Info))
      {
        var line = LogEntryWriter.GenerateLogEntryLine(Info);
        sw.Write(line.ToString());
      }
      sw.Flush();
    }

    #endregion

    #region Private Utility Methods

    /// <summary>
    /// Starts a recurring task that continually runs the log sink flush process until the provider is disposed.
    /// </summary>
    private void RunProcessLogTask()
    {
      Task.Run(() =>
      {
        while (!m_Terminated)
        {
          try
          {
            FlushLogEntryQueue();
            System.Threading.Thread.Sleep(2000); // TODO: Determine if this is should be Task.Delay instead.
          }
          catch(Exception e)
          {
            Console.WriteLine(e.Message); // TODO: consider how to handle low level logging failures e.g. IOExceptions
          }
        }
      });
    }

    #endregion
  }
}
