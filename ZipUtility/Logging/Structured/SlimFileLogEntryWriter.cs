using System;
using System.Collections.Generic;
using System.Linq;
using SRF.FileLogging.Common;
using SRF.FileLogging.Models;

namespace SRF.FileLogging.Structured
{
  /// <summary>
  /// The Slim File Log Entry Writer class that transforms
  /// a LogEntry object to a string line for persistence.
  /// </summary>
  public class SlimFileLogEntryWriter : ILogEntryWriter
  {
    private const char SPACE_CHAR = ' ';
    private const char UNIX_NEWLINE_CHAR = '\n';

    #region Fields and Properties

    private Dictionary<string, int> m_FieldLengths = new Dictionary<string, int>();

    private Tuple<int, int>[] m_Segments = new Tuple<int, int>[8] {
      new Tuple<int, int>(0, 23),
      new Tuple<int, int>(24, 39),
      new Tuple<int, int>(40, 55),
      new Tuple<int, int>(56, 69),
      new Tuple<int, int>(70, 101),
      new Tuple<int, int>(102, 193),
      new Tuple<int, int>(194, 257),
      new Tuple<int, int>(258, 262)
    };

    private char[][] m_HeaderChars = new char[8][]{
       "Time".ToArray(),
       "Host".ToArray(),
       "User".ToArray(),
       "Level".ToArray(),
       "EventId".ToArray(),
       "Category".ToArray(),
       "Scope".ToArray(),
       "Text".ToArray()
      };

    #endregion

    #region ILogEntryWriter implementation methods

    /// <summary>
    /// Generates a <![CDATA[ReadOnlySpan<char>]]> representing a structured format header row.
    /// </summary>
    public ReadOnlySpan<char> GenerateLogHeaderLine()
    {
      var l = new Span<char>(new char[263]);
      l.Fill(SPACE_CHAR);
      for (int i = 0; i < m_Segments.Length; i++)
      {
        var len = Math.Min(m_HeaderChars[i].Length, m_Segments[i].Item2 - m_Segments[i].Item1) + m_Segments[i].Item1;
        var pos = m_Segments[i].Item1;
        for (int j = pos; j < len; j++)
        {
          l[j] = m_HeaderChars[i][j - pos];
        }
      }
      l[^1] = UNIX_NEWLINE_CHAR;
      return l;
    }

    /// <summary>
    /// Generates a <![CDATA[ReadOnlySpan<char>]]> representing a structured format converted LogEntry.
    /// </summary>
    public ReadOnlySpan<char> GenerateLogEntryLine(LogEntry Info)
    {
      char[][] logChars = GetLogEntryArray(Info);

      var lineLen = logChars[7] == null ? 0 : logChars[7].Length;
      var l = new Span<char>(new char[259 + lineLen]);
      l.Fill(SPACE_CHAR);
      for (int i = 0; i < m_Segments.Length - 1; i++)
      {
        var len = Math.Min(logChars[i].Length, m_Segments[i].Item2 - m_Segments[i].Item1) + m_Segments[i].Item1;
        var pos = m_Segments[i].Item1;
        for (int j = pos; j < len; j++)
        {
          l[j] = logChars[i][j - pos];
        }
      }
      // write last line to length of Info.Text (ignore m_Segments length)
      var lenLast = logChars[7].Length + m_Segments[7].Item1;
      var posLast = m_Segments[7].Item1;
      for (int j = posLast; j < lenLast; j++)
      {
        l[j] = logChars[7][j - posLast];
      }
      l[^1] = UNIX_NEWLINE_CHAR;
      return l;
    }

    #endregion

    #region Private Utility Methods

    private static char[][] GetLogEntryArray(LogEntry Info)
    {
      string scope = null;
      if (Info.Scopes != null && Info.Scopes.Count > 0)
      {
        LogScopeInfo SI = Info.Scopes.Last();
        if (!string.IsNullOrWhiteSpace(SI.Text))
        {
          scope = SI.Text;
        }
      }

      var logChars = new char[8][]{
       Info.TimeStampUtc.ToString("yyyy-MM-dd HH:mm:ss.ff").ToArray(),
       Info.HostName.ToArray(),
       Info.UserName.ToArray(),
       Info.Level.ToString().ToArray(),
       Info.EventId != null ? Info.EventId.ToString().ToArray() : new char[0],
       Info.Category.ToArray(),
       string.IsNullOrWhiteSpace(scope) ? new char[0]: scope.ToArray(),
       Info.Text?.ToArray()
      };
      return logChars;
    }

    #endregion
  }
}
