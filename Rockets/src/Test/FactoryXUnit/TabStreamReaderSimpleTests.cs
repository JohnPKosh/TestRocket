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
    public const string TAB_HEADER_SHORT = "country code	postal code	place name	admin name1	admin code1	admin name2	admin code2	admin name3	admin code3	latitude	longitude";

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

      using var inputStream = sut_TabFile.OpenRead();
      using var outputStream = sut_JsonFile.OpenWrite();

      var tsr = new TabStreamReader(inputStream);
      tsr.WriteToSimpleJsonStream(gotHeader, outputStream);

      //Utf8JsonWriter writer = WriteJsonToStream(gotHeader, inputStream, outputStream);


      sut_JsonFile.Refresh();

      /* Assert */
      Assert.True(sut_JsonFile.Exists);
      Assert.True(sut_JsonFile.Length > 0);
    }

    [Fact]
    public void GetTabStreamStringsShort()
    {
      /* Arrange */
      var sut_TabFile = TestConstants.GetSUT_INPUT_FileInfo(TAB_FILE_NAME);
      var sut_JsonFile = TestConstants.GetSUT_OUTPUT_FileInfo(JSON_FILE_NAME);
      if (sut_JsonFile.Exists) sut_JsonFile.Delete();
      var tr = new TabReader();

      /* Act */
      var gotHeader = tr.ReadLineTabJsonStrings(TAB_HEADER_SHORT);

      using var inputStream = sut_TabFile.OpenRead();
      using var outputStream = sut_JsonFile.OpenWrite();

      var tsr = new TabStreamReader(inputStream);
      tsr.WriteToSimpleJsonStream(gotHeader, outputStream);

      //Utf8JsonWriter writer = WriteJsonToStream(gotHeader, inputStream, outputStream);


      sut_JsonFile.Refresh();

      /* Assert */
      Assert.True(sut_JsonFile.Exists);
      Assert.True(sut_JsonFile.Length > 0);
    }

    [Fact]
    public void GetTabStreamStringsWithIEnumerablePropertyNames()
    {
      /* Arrange */
      var sut_TabFile = TestConstants.GetSUT_INPUT_FileInfo(TAB_FILE_NAME);
      var sut_JsonFile = TestConstants.GetSUT_OUTPUT_FileInfo(JSON_FILE_NAME);
      if (sut_JsonFile.Exists) sut_JsonFile.Delete();


      /* Act */
      var gotHeader = new[]
      {
        "country code",
        "postal code",
        "place name",
        "admin name1",
        "admin code1",
        "admin name2",
        "admin code2",
        "admin name3",
        "admin code3",
        "latitude",
        "longitude",
        "accuracy"
      };

      using var inputStream = sut_TabFile.OpenRead();
      using var outputStream = sut_JsonFile.OpenWrite();

      var tsr = new TabStreamReader(inputStream);
      tsr.WriteToSimpleJsonStream(gotHeader, outputStream);

      //Utf8JsonWriter writer = WriteJsonToStream(gotHeader, inputStream, outputStream);


      sut_JsonFile.Refresh();

      /* Assert */
      Assert.True(sut_JsonFile.Exists);
      Assert.True(sut_JsonFile.Length > 0);
    }

    private static Utf8JsonWriter WriteJsonToStream(ReadOnlySpan<JsonEncodedText> propertyNames, Stream inputStream, Stream outputStream)
    {
      var tsr = new TabStreamReader(inputStream);
      var jw = new Utf8JsonWriter(outputStream);

      jw.WriteStartArray();
      foreach (var line in tsr.Utf8ReadLinesToJsonMemory())
      {
        jw.WriteStartObject();
        for (int i = 0; i < propertyNames.Length; i++)
        {
          jw.WriteString(propertyNames[i], line.Span[i]);
        }
        jw.WriteEndObject();
      }
      jw.WriteEndArray();

      jw.Dispose(); // Clean Up
      while (jw.BytesPending > 0)
      {
        System.Threading.Thread.Sleep(5);
      }

      return jw;
    }
  }
}
