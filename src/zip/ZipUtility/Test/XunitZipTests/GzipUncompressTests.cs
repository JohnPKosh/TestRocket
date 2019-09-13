using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using ZipLib.Enums;
using ZipLib.Ext;

namespace XunitZipTests
{
  public class GzipUncompressTests
  {
    private readonly ITestOutputHelper output;

    public GzipUncompressTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void CanGUnzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_02_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_02_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.GUnZip(got, true, 60000);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }
  }
}
