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

    public FileInfo sutFinalFile03 { get; private set; }

    public FileInfo sutControlFile03 { get; private set; }

    public FileInfo sutFile04{ get; private set; }

    public FileInfo sutOutputFile04 { get; private set; }

    public FileInfo sutControlFile04 { get; private set; }

    public FileInfo sutFinalFile04 { get; private set; }


    public GzipRoundTripFixture()
    {
      /* Arrange and initialize some common some stuff here */
      sutFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_03_PATH));
      sutOutputFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_03_PATH));
      sutFinalFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.FINAL_FILE_03_PATH));
      sutControlFile03 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.CONTROL_OUTPUT_FILE_03_PATH));

      sutFile04 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_04_PATH));
      sutOutputFile04 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_04_PATH));
      sutFinalFile04 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.FINAL_FILE_04_PATH));
      sutControlFile04 = new FileInfo(Path.Combine(Environment.CurrentDirectory, TestConstants.CONTROL_OUTPUT_FILE_04_PATH));
    }

    public void Dispose()
    {
      /* clean up the common stuff here */
      if (sutOutputFile03.Exists) sutOutputFile03.Delete();
      if (sutFinalFile03.Exists) sutFinalFile03.Delete();
      if (sutControlFile03.Exists) sutControlFile03.Delete();

      if (sutOutputFile04.Exists) sutOutputFile04.Delete();
      if (sutFinalFile04.Exists) sutFinalFile04.Delete();
      if (sutControlFile04.Exists) sutControlFile04.Delete();
    }
  }
}
