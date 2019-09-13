using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZipLib.Enums;
using ZipLib.Ext;

namespace MSNetZipTests
{
  [TestClass]
  public class BasicTests
  {
    [TestMethod]
    public void CanRun_MSNetTest_True()
    {
      Assert.IsTrue(true); /* Sanity check to make sure test framework is working */
    }

    [TestMethod]
    public void CanGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);

      /* Act */
      Trace.WriteLine("File already exists:{0}", got.Exists.ToString());
      got = gotInput.GZipCompress(got, 60000);

      /* Assert */
      Trace.WriteLine("File created:{0}", got?.FullName);
      Assert.IsTrue(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }

  }
}
