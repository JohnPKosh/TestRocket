using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Collections.Concurrent;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace SRF.FileLogger
{
  /// <summary>
  /// A logger provider that writes log entries to a text file.
  /// <para>"File" is the provider alias of this provider and can be used in the Logging section of the appsettings.json.</para>
  /// </summary>
  [ProviderAlias("File")]
  public class FileLoggerProvider : LoggerProvider
  {
    private bool m_Terminated;
    private int m_Counter = 0;
    private string m_FilePath;
    private Dictionary<string, int> m_FieldLengths = new Dictionary<string, int>();
    private ConcurrentQueue<LogEntry> m_LogEntryQueue = new ConcurrentQueue<LogEntry>();

    /// <summary>The FileLoggerOptions property passed into the constructor.</summary>
    internal FileLoggerOptions LoggerOptions { get; private set; }

    #region Constructors

    /// <summary>
    /// Constructor accepting a <![CDATA[IOptionsMonitor<FileLoggerOptions>]]> that passes the current settings to the default constructor.
    /// <see cref=": https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens"/>
    /// <para>The IOptionsMonitor provides the OnChange() method which is called when the user alters the settings of this provider in the appsettings.json file.</para>
    /// </summary>
    public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options) : this(options.CurrentValue)
    {
      SettingsChangeToken = options.OnChange(changedOptions =>
      {
        LoggerOptions = changedOptions;
      });
    }

    /// <summary>The default constructor accepting a FileLoggerOptions.</summary>
    public FileLoggerProvider(FileLoggerOptions options)
    {
      LoggerOptions = options;
      Initialize();
    }

    #endregion


    #region LoggerProvider Overrides

    protected override void Initialize()
    {
      PrepareLengths();
      BeginFile(); // create the first file
      ThreadProc();
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

    #region Private Utility Methods

    /// <summary>Applies the log file retains policy according to options</summary>
    void ApplyRetainPolicy()
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
      catch{ }
    }

    /// <summary>
    /// Writes a line of text to the current file.
    /// If the file reaches the size limit, creates a new file and uses that new file.
    /// </summary>
    void WriteLine(string Text)
    {
      // check the file size after any 100 writes
      m_Counter++;
      if (m_Counter % 100 == 0)
      {
        FileInfo FI = new FileInfo(m_FilePath);
        if (FI.Length > (1024 * 1024 * LoggerOptions.MaxFileSizeInMB))
        {
          BeginFile();
        }
      }
      File.AppendAllText(m_FilePath, Text);
    }

    /// <summary>
    /// Pads a string with spaces to a max length. Truncates the string to max length if the string exceeds the limit.
    /// </summary>
    string Pad(string Text, int MaxLength)
    {
      if (string.IsNullOrWhiteSpace(Text))
        return "".PadRight(MaxLength);

      if (Text.Length > MaxLength)
        return Text.Substring(0, MaxLength);

      return Text.PadRight(MaxLength);
    }

    /// <summary>Prepares the lengths of the columns in the log file.</summary>
    void PrepareLengths()
    {
      // prepare the lengths table
      m_FieldLengths["Time"] = 24;
      m_FieldLengths["Host"] = 16;
      m_FieldLengths["User"] = 16;
      m_FieldLengths["Level"] = 14;
      m_FieldLengths["EventId"] = 32;
      m_FieldLengths["Category"] = 92;
      m_FieldLengths["Scope"] = 64;
    }

    /// <summary>
    /// Creates a new disk file and writes the column titles
    /// </summary>
    void BeginFile()
    {
      Directory.CreateDirectory(LoggerOptions.Folder);
      m_FilePath = Path.Combine(LoggerOptions.Folder, LogEntry.StaticHostName + "-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".log");

      // titles
      StringBuilder SB = new StringBuilder();
      SB.Append(Pad("Time", m_FieldLengths["Time"]));
      SB.Append(Pad("Host", m_FieldLengths["Host"]));
      SB.Append(Pad("User", m_FieldLengths["User"]));
      SB.Append(Pad("Level", m_FieldLengths["Level"]));
      SB.Append(Pad("EventId", m_FieldLengths["EventId"]));
      SB.Append(Pad("Category", m_FieldLengths["Category"]));
      SB.Append(Pad("Scope", m_FieldLengths["Scope"]));
      SB.AppendLine("Text");

      File.WriteAllText(m_FilePath, SB.ToString());

      ApplyRetainPolicy();
    }

    /// <summary>
    /// Pops a log info instance from the stack, prepares the text line, and writes the line to the text file.
    /// </summary>
    void WriteLogLine()
    {
      if (m_LogEntryQueue.TryDequeue(out LogEntry Info))
      {
        string S;
        StringBuilder SB = new StringBuilder();
        SB.Append(Pad(Info.TimeStampUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.ff"), m_FieldLengths["Time"]));
        SB.Append(Pad(Info.HostName, m_FieldLengths["Host"]));
        SB.Append(Pad(Info.UserName, m_FieldLengths["User"]));
        SB.Append(Pad(Info.Level.ToString(), m_FieldLengths["Level"]));
        SB.Append(Pad(Info.EventId != null ? Info.EventId.ToString() : "", m_FieldLengths["EventId"]));
        SB.Append(Pad(Info.Category, m_FieldLengths["Category"]));

        S = "";
        if (Info.Scopes != null && Info.Scopes.Count > 0)
        {
          LogScopeInfo SI = Info.Scopes.Last();
          if (!string.IsNullOrWhiteSpace(SI.Text))
          {
            S = SI.Text;
          }
          else
          {
          }
        }
        SB.Append(Pad(S, m_FieldLengths["Scope"]));

        string Text = Info.Text;

        /* writing properties is too much for a text file logger
        if (Info.StateProperties != null && Info.StateProperties.Count > 0)
        {
            Text = Text + " Properties = " + Newtonsoft.Json.JsonConvert.SerializeObject(Info.StateProperties);
        }
         */

        if (!string.IsNullOrWhiteSpace(Text))
        {
          SB.Append(Text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " "));
        }

        SB.AppendLine();
        WriteLine(SB.ToString());
      }
    }

    void ThreadProc()
    {
      Task.Run(() =>
      {
        while (!m_Terminated)
        {
          try
          {
            WriteLogLine();
            System.Threading.Thread.Sleep(100);
          }
          catch // (Exception ex)
          {
          }
        }
      });
    }

    #endregion
  }
}
