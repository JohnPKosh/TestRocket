using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ZipLib.Logging
{
  public static class LogTextWriter
  {

    public static void AppendLog(string text)
    {
      LogTextWriterSingleton.Instance.AppendLog(text);
    }
  }

  internal class LogTextWriterSingleton
  {
    private static readonly Lazy<LogTextWriterSingleton> lazy = new Lazy<LogTextWriterSingleton>(() => new LogTextWriterSingleton());

    public static LogTextWriterSingleton Instance { get { return lazy.Value; } }

    private static object locker = new object();

    internal LogTextWriterSingleton()
    {
      m_Stream = File.AppendText("log.txt");
    }

    private StreamWriter m_Stream;

    internal void AppendLog(string text)
    {
      try
      {
        lock (locker)
        {
          m_Stream.WriteLine(text);
          m_Stream.Flush();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        //throw;
      }
    }
  }
}
