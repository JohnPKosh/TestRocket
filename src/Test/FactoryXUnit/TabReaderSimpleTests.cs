using System;
using System.Linq;
using Xunit;
using Factory.Util;
using RocketWriter;
using System.IO;
using System.Text.Json;

namespace FactoryXUnit
{
  public class TabReaderSimpleTests
  {
    public const string JSON_FILE_PATH = @"C:\vscode\github\TestRocket\src\Test\FactoryXUnit\tab-json-strings.json";

    public const string TAB_HEADER_SIMPLE = "country code	postal code	place name	admin name1	admin code1	admin name2	admin code2	admin name3	admin code3	latitude	longitude	accuracy";

    public const string TAB_LINE_SIMPLE = "US	99591	Saint George Island	Alaska	AK	Aleutians West (CA)	016			56.5944	-169.6186	1";

    [Fact]
    public void GetTabStrings()
    {
      /* Arrange */
      var tr = new TabReader();

      /* Act */
      var got = tr.GetTabStrings();
      Console.WriteLine(got.ToArray().ToPrettyString()); // TODO: replace with output.WriteLine logic

      /* Assert */
      Assert.False(got.IsEmpty);
    }


    [Fact]
    public void GetTabJsonStrings()
    {
      /* Arrange */
      var tr = new TabReader();
      var jsonFile = new FileInfo(JSON_FILE_PATH);

      /* Act */
      var got = tr.GetTabJsonStrings();
      var gotHeader = tr.GetTabJsonStrings(TAB_HEADER_SIMPLE);

      using var writefs = jsonFile.OpenWrite();
      using var writer = new Utf8JsonWriter(writefs);

      writer.WriteStartArray();
      writer.WriteStartObject();
      for (int i = 0; i < gotHeader.Length; i++)
      {
        writer.WriteString(gotHeader[i], got[i]);
      }


      writer.WriteEndObject();
      writer.WriteEndArray();
      // await writer.FlushAsync();
      writer.Dispose();
      while (writer.BytesPending > 0)
      {
        System.Threading.Thread.Sleep(5);
      }
      jsonFile.Refresh();

      /* Assert */
      Assert.False(got.IsEmpty);
    }

  }
}
