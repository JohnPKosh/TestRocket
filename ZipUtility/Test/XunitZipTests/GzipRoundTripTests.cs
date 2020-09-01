using System.IO;
using Xunit;
using Xunit.Abstractions;
using XunitZipTests.Fixtures;
using ZipLib.Enums;
using ZipLib.Ext;

namespace XunitZipTests
{
  public class GzipRoundTripTests : IClassFixture<GzipRoundTripFixture>
  {
    private readonly ITestOutputHelper output;
    private readonly GzipRoundTripFixture m_Fixture;

    public GzipRoundTripTests(ITestOutputHelper output, GzipRoundTripFixture fixture)
    {
      this.output = output;
      this.m_Fixture = fixture;
    }

    [Fact]
    public void CanRoundTrip_Test01_True()
    {
      /* Arrange */
      var gotInput = m_Fixture.sutFile03;
      var gotOutput = m_Fixture.sutOutputFile03;
      var gotFinal = m_Fixture.sutFinalFile03;
      var gotControl = m_Fixture.sutControlFile03;

      /* Act */
      gotOutput = gotInput.Compress(gotOutput, 60000, ExistingFileHandling.Overwrite);
      output.WriteLine("Compressed File created:{0} with {1} bytes", gotOutput?.FullName, gotOutput?.Length);
      gotFinal = gotOutput.Decompress(gotFinal, onExisting: ExistingFileHandling.Overwrite);

      /* Assert */
      Assert.True(gotFinal.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotFinal?.FullName}");

      output.WriteLine("File created:{0} with {1} bytes", gotFinal?.FullName, gotFinal?.Length);
      Assert.True(gotFinal.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotFinal?.FullName}");
      Assert.Equal(gotControl.Length, gotFinal.Length);
      output.WriteLine("File matches:{0} with {1} bytes", gotControl?.FullName, gotControl?.Length);
    }

    [Fact]
    public void CanRoundTripMissingExt_Test01_True()
    {
      /* Arrange */
      var gotInput = m_Fixture.sutFile04;
      var gotOutput = m_Fixture.sutOutputFile04;
      var gotFinal = m_Fixture.sutFinalFile04;
      var gotControl = m_Fixture.sutControlFile04;

      /* Act */
      gotOutput = gotInput.Compress(gotOutput, 60000, ExistingFileHandling.Overwrite);
      output.WriteLine("Compressed File created:{0} with {1} bytes", gotOutput?.FullName, gotOutput?.Length);
      gotFinal = gotOutput.Decompress(gotFinal, onExisting: ExistingFileHandling.Overwrite);

      /* Assert */
      Assert.True(gotFinal.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotFinal?.FullName}");

      output.WriteLine("File created:{0} with {1} bytes", gotFinal?.FullName, gotFinal?.Length);
      Assert.True(gotFinal.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotFinal?.FullName}");
      Assert.Equal(gotControl.Length, gotFinal.Length);
      output.WriteLine("File matches:{0} with {1} bytes", gotControl?.FullName, gotControl?.Length);
    }


    [Fact]
    public void CanRoundTripNoDir_Test01_True()
    {
      /* Arrange */
      var gotInput = m_Fixture.sutFile04;
      var gotOutput = new FileInfo(m_Fixture.sutOutputFile04.Name); // just want to provide just a file name with no dir here
      var gotFinal = m_Fixture.sutFinalFile04;
      var gotControl = m_Fixture.sutControlFile04;

      /* Act */
      gotOutput = gotInput.Compress(gotOutput, 60000, ExistingFileHandling.Overwrite);
      output.WriteLine("Compressed File created:{0} with {1} bytes", gotOutput?.FullName, gotOutput?.Length);
      gotFinal = gotOutput.Decompress(gotFinal, onExisting: ExistingFileHandling.Overwrite);

      /* Assert */
      Assert.True(gotFinal.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotFinal?.FullName}");

      output.WriteLine("File created:{0} with {1} bytes", gotFinal?.FullName, gotFinal?.Length);
      Assert.True(gotFinal.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {gotFinal?.FullName}");
      Assert.Equal(gotControl.Length, gotFinal.Length);
      output.WriteLine("File matches:{0} with {1} bytes", gotControl?.FullName, gotControl?.Length);
    }


  }
}
