using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRF.FileLogging.Common;
using SRF.FileLogging.Models;

namespace SRF.FileLogging.Structured
{
  public class SlimFileLogEntryWriter : ILogEntryWriter
  {
    private Dictionary<string, int> m_FieldLengths = new Dictionary<string, int>();

    public SlimFileLogEntryWriter()
    {
      PrepareLengths();
    }

    #region ILogEntryWriter implementation methods

    public string GenerateLogEntryLine(LogEntry Info)
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
      //WriteEntryToSink(SB.ToString());
      return SB.ToString();
    }

    public string GenerateLogHeaderLine()
    {
      StringBuilder SB = new StringBuilder();
      SB.Append(Pad("Time", m_FieldLengths["Time"]));
      SB.Append(Pad("Host", m_FieldLengths["Host"]));
      SB.Append(Pad("User", m_FieldLengths["User"]));
      SB.Append(Pad("Level", m_FieldLengths["Level"]));
      SB.Append(Pad("EventId", m_FieldLengths["EventId"]));
      SB.Append(Pad("Category", m_FieldLengths["Category"]));
      SB.Append(Pad("Scope", m_FieldLengths["Scope"]));
      SB.AppendLine("Text");
      return SB.ToString();
    }

    #endregion

    #region private utility methods

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

    #endregion

  }
}
