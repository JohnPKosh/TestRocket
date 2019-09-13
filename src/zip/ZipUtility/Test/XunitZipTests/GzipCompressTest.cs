using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using ZipLib.Enums;
using ZipLib.Ext;

namespace XunitZipTests
{
  public class GzipCompressTest
  {
    private readonly ITestOutputHelper output;

    public GzipCompressTest(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void CanRun_xUnit_True()
    {
      Assert.True(true); /* Sanity check to make sure test framework is working */
    }

    [Fact]
    public void CanFindFile_Test01_True()
    {
      /* Arrange */
      var gotFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);

      /* Act */
      var got = new FileInfo(gotFilePath);

      /* Assert */
      Assert.True(got.Exists);
    }

    [Fact]    
    public void CanCopyFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutCopyFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_COPY_PATH); 

      var gotInput = new FileInfo(sutFilePath);
      var gotCopy = new FileInfo(sutCopyFilePath);

      try
      {
        /* Act */
        File.Copy(gotInput.FullName, gotCopy.FullName);

        /* Assert */
        Assert.True(gotCopy.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotCopy.FullName}");
      }
      finally
      {
        if (gotCopy != null && gotCopy.Exists) File.Delete(gotCopy.FullName);
      }
    }

    [Fact]
    public void CanGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.GZip(got, 60000);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }

    [Fact]
    public void CanDeleteAndGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got= new FileInfo(sutOutputFilePath);

      /* Act */
      if (got != null && got.Exists)
      {
        File.Delete(got.FullName);
        got.Refresh();
      }
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.GZip(got, 60000);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }

    [Fact]
    public void CanOverwriteGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.GZip(got, 60000, ExistingFileHandling.Overwrite);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got?.FullName}");
    }

    [Fact]
    public void CanReplaceAndArchiveGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.GZip(got, 60000, ExistingFileHandling.ReplaceAndArchive);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }

    [Theory]
    [InlineData(ExistingFileHandling.ReplaceAndArchive)]
    [InlineData(ExistingFileHandling.Overwrite)]
    [InlineData(ExistingFileHandling.PreserveExisting)]
    public void CanEnumerateGzipFile_Test01_True(ExistingFileHandling handling)
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.GZip(got, 60000, handling);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }

  }
}
