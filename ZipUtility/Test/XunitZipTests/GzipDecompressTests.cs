using System.IO;
using Xunit;
using Xunit.Abstractions;
using XunitZipTests.Fixtures;
using ZipLib.Enums;
using ZipLib.Ext;

namespace XunitZipTests
{
  public class GzipDecompressTests : IClassFixture<GzipDecompressFixture>
  {
    private readonly ITestOutputHelper output;
    private readonly GzipDecompressFixture m_Fixture;

    public GzipDecompressTests(ITestOutputHelper output, GzipDecompressFixture fixture)
    {
      this.output = output;
      this.m_Fixture = fixture;
    }

    [Fact]
    public void CanDecompressFile_Test02_True()
    {
      /* Arrange */
      var gotInput = m_Fixture.sutInputFileInfo02;
      var got = m_Fixture.sutOutputFileInfo02;

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.Decompress(got);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }


    [Fact]
    public void CanDecompressOverwriteFile_Test02_True()
    {
      /* Arrange */
      var gotInput = m_Fixture.sutInputFileInfo02;
      var got = m_Fixture.sutOutputFileInfo02;

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.Decompress(got, onExisting: ExistingFileHandling.Overwrite);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
    }


    [Fact]
    public void CanDecompressThrowFile_Test02_True()
    {
      /* Arrange */
      var gotInput = m_Fixture.sutInputFileInfo02;
      var got = m_Fixture.sutOutputFileInfo02;

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      var ex = Assert.Throws<IOException>(() => gotInput.Decompress(got, onExisting: ExistingFileHandling.ThrowException));

      /* Assert */
      Assert.NotNull(ex);
      output.WriteLine("Would have thrown this exception: {0}", ex.Message);
    }

    [Theory]
    [InlineData(ExistingFileHandling.ReplaceAndArchive)]
    [InlineData(ExistingFileHandling.Overwrite)]
    [InlineData(ExistingFileHandling.PreserveExisting)]
    public void CanEnumerateDecompressFile_Test02_True(ExistingFileHandling handling)
    {
      /* Arrange */
      var gotInput = m_Fixture.sutInputFileInfo02;
      var got = m_Fixture.sutOutputFileInfo02;

      /* Act */
      output.WriteLine("File already exists:{0}", got.Exists);
      got = gotInput.Decompress(got, onExisting: handling);

      /* Assert */
      output.WriteLine("File created:{0}", got?.FullName);
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got.FullName}");
      Assert.True(got.Length == m_Fixture.sutControlOutputFileInfo02.Length);
    }

  }
}
