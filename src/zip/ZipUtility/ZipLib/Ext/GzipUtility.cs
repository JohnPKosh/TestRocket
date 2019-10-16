using System.IO;
using System.IO.Compression;
using ZipLib.Enums;
using System;

namespace ZipLib.Ext
{
  public static class GzipUtility
  {
    #region Public Methods

    public static FileInfo GZip(this FileInfo inputFile,
      FileInfo outputFile,
      GZipType compressionMode,
      int lockWaitMs = 60000,
      ExistingFileHandling onExisting = ExistingFileHandling.PreserveExisting,
      int bufferSize = 4096)
    {
      VerifyFileParams(inputFile, outputFile);
      FileInfo m_OutputFileInfo = EnsureOutputDirectory(inputFile, outputFile);

      if (inputFile.FullName.Equals(m_OutputFileInfo.FullName, StringComparison.InvariantCultureIgnoreCase))
        throw new IOException($"{nameof(inputFile)} ({inputFile.FullName}) and {nameof(outputFile)} ({m_OutputFileInfo.FullName}) cannot be the same!");

      if (m_OutputFileInfo.Exists)
      {
        switch (onExisting)
        {
          case ExistingFileHandling.PreserveExisting:
            return m_OutputFileInfo;
          case ExistingFileHandling.Overwrite:
            m_OutputFileInfo.Delete();
            break;
          case ExistingFileHandling.ReplaceAndArchive:
            var m_arcFile = GetArchiveFullName(m_OutputFileInfo);
            if (!File.Exists(m_arcFile)) File.Move(m_OutputFileInfo.FullName, m_arcFile);
            else m_OutputFileInfo.Delete(); /* may no longer be numerically possible needs validation */
            break;
          default:
            throw new IOException(IoConstants.FILE_EXISTS_ERROR_MSG);
        }
      }
      if (compressionMode == GZipType.Compress) return ExecuteCompress(inputFile, m_OutputFileInfo, bufferSize, lockWaitMs);
      return ExecuteDecompress(inputFile, outputFile, bufferSize, lockWaitMs);
    }

    public static FileInfo Compress(this FileInfo inputFile,
      FileInfo outputFile,
      int lockWaitMs = 60000,
      ExistingFileHandling onExisting = ExistingFileHandling.PreserveExisting,
      int bufferSize = 4096)
    {
      return GZip(inputFile, outputFile, GZipType.Compress, lockWaitMs, onExisting, bufferSize);
    }

    public static FileInfo Decompress(this FileInfo inputFile,
      FileInfo outputFile,
      int lockWaitMs = 60000,
      ExistingFileHandling onExisting = ExistingFileHandling.PreserveExisting,
      int bufferSize = 4096)
    {
      return GZip(inputFile, outputFile, GZipType.Decompress, lockWaitMs, onExisting, bufferSize);
    }

    public static void CompressStream(int bufferSize, Stream instream, Stream outstream, bool leaveOpen = false)
    {
      using (var stream = new GZipStream(outstream, mode: CompressionMode.Compress, leaveOpen))
      {
        instream.CopyTo(stream, bufferSize);
      }
    }

    public static void DecompressStream(int bufferSize, Stream instream, Stream outstream, bool leaveOpen = false)
    {
      using (var stream = new GZipStream(instream, CompressionMode.Decompress, leaveOpen))
      {
        stream.CopyTo(outstream, bufferSize);
      }
    }

    #endregion

    #region Private Utility Methods

    private static FileInfo ExecuteDecompress(
      FileInfo inputFile,
      FileInfo outputFile,
      int bufferSize,
      int lockWaitMs)
    {
      using (var instream = inputFile.OpenFileStream(
        FileMode.Open,
        FileAccess.Read,
        FileShare.Delete | FileShare.Read,
        bufferSize,
        FileOptions.SequentialScan,
        lockWaitMs,
        false))
      {
        using (var outstream = new FileStream(
          outputFile.FullName,
          FileMode.Create,
          FileAccess.Write,
          FileShare.None,
          bufferSize))
        {
          DecompressStream(bufferSize, instream, outstream);
        }
      }
      outputFile.Refresh();
      return outputFile;
    }
    
    private static FileInfo ExecuteCompress(
      FileInfo inputFile,
      FileInfo m_OutputFileInfo,
      int bufferSize,
      int lockWaitMs)
    {
      using (var inputfs = inputFile.OpenFileStream(FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan, lockWaitMs, false))
      {
        using (var outputfs = m_OutputFileInfo.OpenFileStream(FileMode.CreateNew, FileAccess.Write, FileShare.None, 60000, true))
        {
          CompressStream(bufferSize, inputfs, outputfs);
        }
      }
      m_OutputFileInfo.Refresh();
      return m_OutputFileInfo;
    }

    private static void VerifyFileParams(FileInfo inputFile, FileInfo outputFile)
    {
      if (inputFile == null)
      {
        throw new ArgumentNullException(nameof(inputFile));
      }
      if (outputFile == null)
      {
        throw new ArgumentNullException(nameof(outputFile));
      }
      inputFile.Refresh();
      if (!inputFile.Exists) throw new IOException(IoConstants.IO_MISSING_FILE_MSG + inputFile.FullName);
      outputFile.Refresh();
    }

    private static FileInfo EnsureOutputDirectory(FileInfo inputFile, FileInfo outputFile)
    {
      /* TODO: determine best approach to allow other whitelisted directories.
       * We don't want to expose any potentially malicous file system access
       * or allow any dangerous bugs to cause system issues.
       */
      return !outputFile.DirectoryName.ToUpperInvariant().Contains(inputFile.DirectoryName.ToUpperInvariant())
        ? new FileInfo(Path.Combine(inputFile.DirectoryName, outputFile.Name))
        : outputFile;
    }

    private static string GetArchiveFullName(FileInfo fileInfo)
    {
      var m_lastWrite = fileInfo.Exists ? fileInfo.LastWriteTimeUtc.ToString("yyyyMMddHHmmssfff") : DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
      if (string.IsNullOrWhiteSpace(fileInfo.Extension)) return fileInfo.FullName + $".{m_lastWrite}{fileInfo.Extension}";

      var m_NewName = GetArchiveName(fileInfo, m_lastWrite);
      if (File.Exists(Path.Combine(fileInfo.DirectoryName, m_NewName)))
      {
        m_NewName = GetArchiveName(fileInfo, DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString().Substring(0, 5));
      }
      return Path.Combine(fileInfo.DirectoryName, m_NewName);
    }

    private static string GetArchiveName(FileInfo fileInfo, string m_lastWrite)
    {
      var m_extidx = fileInfo.Name.LastIndexOf(fileInfo.Extension);
      var xname = fileInfo.Name.Substring(0, m_extidx < 0 ? fileInfo.Name.Length : m_extidx) + $".{m_lastWrite}{fileInfo.Extension}";
      return xname;
    }

    #endregion
  }
}
