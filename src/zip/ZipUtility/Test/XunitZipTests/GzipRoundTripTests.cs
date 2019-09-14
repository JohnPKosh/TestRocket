using System;
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
      var got = m_Fixture.sutOutputFile03;
      var gotCompare = m_Fixture.sutCompareFile03;

      /* Act */
      got = gotInput.GZipCompress(got, 60000, ExistingFileHandling.Overwrite);
      gotCompare = got.GZipDecompress(gotCompare, onExisting: ExistingFileHandling.Overwrite);

      /* Assert */
      Assert.True(got.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got?.FullName}");

      output.WriteLine("File created:{0} with {1} bytes", gotCompare?.FullName, gotCompare?.Length);
      Assert.True(gotCompare.Exists, $"{TestConstants.CANNOT_FIND_FILE_MSG} {got?.FullName}");

      Assert.Equal(gotInput.Length, gotCompare.Length);
    }


  }
}
