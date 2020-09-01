using System;
using System.IO;
using System.Linq;

namespace XunitZipTests.Fixtures
{
  public class GzipDecompressFixture : IDisposable
  {
    public FileInfo sutControlInputFileInfo02 { get; private set; }

    public FileInfo sutControlOutputFileInfo02 { get; private set; }

    public FileInfo sutInputFileInfo02 { get; private set; }

    public FileInfo sutOutputFileInfo02 { get; private set; }

    public GzipDecompressFixture()
    {
      /* Arrange and initialize some common some stuff here */

      sutControlInputFileInfo02 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.CONTROL_INPUT_FILE_02_PATH));
      sutControlOutputFileInfo02 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.CONTROL_OUTPUT_FILE_02_PATH));

      sutInputFileInfo02 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_02_PATH));
      sutOutputFileInfo02 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_02_PATH));
    }

    public void Dispose()
    {
      /* clean up the common stuff here */

      /* Move below to GzipCompressFixture once one is created */

      var m_CleanDir01 = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.CLEAN_UP_DIR_01));
      var m_Contains = $".{DateTime.Today.ToString("yyyyMM")}";

      foreach (var f in m_CleanDir01.EnumerateFiles().Where(x => x.Name.Contains(m_Contains)))
      {
        f.Delete();
      }
    }

  }

}
