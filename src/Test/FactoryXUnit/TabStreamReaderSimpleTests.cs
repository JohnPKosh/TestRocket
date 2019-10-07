using System;
using System.Linq;
using Xunit;
using Factory.Util;
using RocketWriter;
using System.IO;
using System.Text.Json;
using System.Text;

namespace FactoryXUnit
{
  public class TabStreamReaderSimpleTests
  {
    public const string TAB_FILE_NAME = @"AK.txt";

    public const string TAB_HEADER_SIMPLE = "country code	postal code	place name	admin name1	admin code1	admin name2	admin code2	admin name3	admin code3	latitude	longitude	accuracy";

    public const string JSON_FILE_NAME = @"stream-tests-01.json";

    [Fact]
    public void GetTabStreamStrings()
    {
      /* Arrange */
      var sut_TabFile = TestConstants.GetSUT_INPUT_FileInfo(TAB_FILE_NAME);
      var sut_JsonFile = TestConstants.GetSUT_OUTPUT_FileInfo(JSON_FILE_NAME);
      if (sut_JsonFile.Exists) sut_JsonFile.Delete();
      var tr = new TabReader();

      /* Act */
      var gotHeader = tr.ReadLineTabJsonStrings(TAB_HEADER_SIMPLE);

      using var fs = sut_TabFile.OpenRead();
      using var writefs = sut_JsonFile.OpenWrite();
      using var writer = new Utf8JsonWriter(writefs);

      var tsr = new TabStreamReader(fs);
      writer.WriteStartArray();

      foreach (var line in tsr.Utf8ReadLinesToJsonMemory())
      {
        writer.WriteStartObject();
        for (int i = 0; i < gotHeader.Length; i++)
        {
          writer.WriteString(gotHeader[i], line.Span[i]);
        }
        writer.WriteEndObject();
      }

      writer.WriteEndArray();

      // Clean Up
      writer.Dispose();
      while (writer.BytesPending > 0)
      {
        System.Threading.Thread.Sleep(5);
      }
      sut_JsonFile.Refresh();

      /* Assert */
      Assert.True(sut_JsonFile.Exists);
      Assert.True(sut_JsonFile.Length > 0);
    }

  }
}
