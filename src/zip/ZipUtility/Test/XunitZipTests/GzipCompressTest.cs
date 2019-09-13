using System;
using System.IO;
using Xunit;
using ZipLib.Enums;
using ZipLib.Ext;

namespace XunitZipTests
{
  public class GzipCompressTest
  {
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
      var got= new FileInfo(sutOutputFilePath);

      try
      {
        /* Act */
        if (got != null && got.Exists) File.Delete(got.FullName);
        got = gotInput.GZip(got, 60000);

        /* Assert */
        Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
      }
      finally
      {
        if (got != null && sutOutputFilePath != got.FullName && got.Exists) File.Delete(got.FullName);
      }
    }

    [Fact]
    public void CanOverwriteGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);
      var gotExists = got.Exists;

      try
      {
        /* Act */
        got = gotInput.GZip(got, 60000, ExistingFileHandling.Overwrite);

        /* Assert */
        Assert.True(gotExists);
        Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
      }
      finally
      {
        if (got != null && sutOutputFilePath != got.FullName && got.Exists) File.Delete(got.FullName);
      }
    }


    [Fact]
    public void CanReplaceAndArchiveGzipFile_Test01_True()
    {
      /* Arrange */
      var sutFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.INPUT_FILE_01_PATH);
      var sutOutputFilePath = Path.Combine(Environment.CurrentDirectory, TestConstants.OUTPUT_FILE_01_PATH);
      var gotInput = new FileInfo(sutFilePath);
      var got = new FileInfo(sutOutputFilePath);
      var gotExists = got.Exists;

      try
      {
        /* Act */
        got = gotInput.GZip(got, 60000, ExistingFileHandling.ReplaceAndArchive);

        /* Assert */
        Assert.True(gotExists);
        Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
      }
      finally
      {
        if (got != null && sutOutputFilePath != got.FullName && got.Exists) File.Delete(got.FullName);
      }
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
      var gotExists = got.Exists;

      try
      {
        /* Act */
        got = gotInput.GZip(got, 60000, handling);

        /* Assert */
        Assert.True(gotExists);
        Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
      }
      finally
      {
        if (got != null && sutOutputFilePath != got.FullName && got.Exists) File.Delete(got.FullName);
      }
    }

  }
}
