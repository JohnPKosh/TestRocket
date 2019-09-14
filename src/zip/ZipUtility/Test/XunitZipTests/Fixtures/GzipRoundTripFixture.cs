using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XunitZipTests.Fixtures
{
  public class GzipRoundTripFixture : IDisposable
  {
    public FileInfo sutFile03 { get; private set; }

    public FileInfo sutOutputFile03 { get; private set; }

    public FileInfo sutCompareFile03 { get; private set; }


    public GzipRoundTripFixture()
    {
      /* Arrange and initialize some common some stuff here */
      sutFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_03_PATH));
      sutOutputFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_03_PATH));
      sutCompareFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.COMPARE_FILE_03_PATH));
    }

    public void Dispose()
    {
      /* clean up the common stuff here */
      if (sutOutputFile03.Exists) sutOutputFile03.Delete();
      if (sutCompareFile03.Exists) sutCompareFile03.Delete();
    }
  }
}
