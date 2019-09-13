using System;
using System.IO;
using System.Threading;

namespace ZipLib.Ext
{
  public static class FileInfoUtility
  {

    public static FileStream OpenFileStream(this FileInfo file, FileMode mode, FileAccess access, FileShare share, int lockWaitMs, bool lockStream)
    {
      var lockWaitUntil = DateTime.Now.AddMilliseconds(lockWaitMs);
      while (true)
      {
        try
        {
          var stream = new FileStream(file.FullName, mode, access, share);
          if (lockStream) stream.Lock(0, stream.Length);
          return stream;
        }
        catch (IOException ex)
        {
          if (!ex.Message.Contains(IoConstants.IO_ACCESS_ERROR_MSG) || DateTime.Now >= lockWaitUntil) throw;
          Thread.Sleep(500);
        }
      }
    }

    public static FileStream OpenFileStream(this FileInfo file, FileMode mode, FileAccess access, FileShare share, int bufferSize, int lockWaitMs, bool lockStream)
    {
      var lockWaitUntil = DateTime.Now.AddMilliseconds(lockWaitMs);
      while (true)
      {
        try
        {
          var stream = new FileStream(file.FullName, mode, access, share, bufferSize);
          if (lockStream) stream.Lock(0, stream.Length);
          return stream;
        }
        catch (IOException ex)
        {
          if (!ex.Message.Contains(IoConstants.IO_ACCESS_ERROR_MSG) || DateTime.Now >= lockWaitUntil) throw;
          Thread.Sleep(500);
        }
      }
    }
  }
}
