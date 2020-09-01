using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

using SRF;
using SRF.IO;
using System.Reflection;
using System.Linq;
using SRF.Models;

namespace XunitZipTests
{
  public class SRFTests
  {
    private readonly ITestOutputHelper output;

    public SRFTests(ITestOutputHelper output)
    {
      this.output = output;
    }


    [Fact]
    public void CanGetLogDirectoryName_True()
    {
      var got = IoConfig.AppData.FullName;
      output.WriteLine(got);
      Assert.NotNull(got);
    }


    [Fact]
    public void CanGetLogDirectoryExists_True()
    {
      var got = IoConfig.AppData.Exists;
      output.WriteLine(got.ToString());
      Assert.NotNull(got.ToString());
    }

    [Fact]
    public void CanReadLibraryResource()
    {
      var got = ResourceExtensions.ReadLibraryResource(@"res\license.txt");
      output.WriteLine(got);
      Assert.NotNull(got);
    }

    //test-file-01.txt

    [Fact]
    public void CanReadEmbeddedResource()
    {
      var got = ResourceExtensions.ReadEmbeddedResource(@"res\test-file-01.txt", Assembly.GetExecutingAssembly());
      output.WriteLine(got);
      Assert.NotNull(got);
    }

    [Fact]
    public void CanAccessConfigurationAssemblyConnectionStrings()
    {
      var cname = IoConfig.ConfigurationAssembly.GetName();
      IoConfig.ConfigurationAssembly = Assembly.GetExecutingAssembly();
      var nm = IoConfig.ConfigurationAssembly.GetName();
      IoConfigSettings.Instance.Config.ResourcePrefix = "XunitZipTests";
      var LibraryResourceNames = new HashSet<string>(Assembly.GetExecutingAssembly().GetManifestResourceNames());

      var configs = ConnectionStringConfig.CurrentConfig.Items;
      Assert.True(configs.Any());

      var allconfig = ConnectionStringConfig.Settings;
      Assert.True(allconfig["SIT"].Items.Any());
      Assert.True(allconfig["UAT"].Items.Any());
      Assert.True(allconfig["PROD"].Items.Any());
    }
  }
}
