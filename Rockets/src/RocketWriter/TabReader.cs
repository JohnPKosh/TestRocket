using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RocketWriter
{
  public class TabReader
  {

    /// <summary>
    /// Splits a string based on tab characters to a ReadOnlySpan of strings.
    /// TODO: Review actual usefullness of this since there are likely more efficient
    /// ways to do this byte or stream buffer reading. May apply for header row?
    /// </summary>
    /// <returns><![CDATA[ReadOnlySpan<string>]]></returns>
    public ReadOnlySpan<string> ReadLineTabStrings(string value)
    {
      return new ReadOnlySpan<string>(value.Split('\t', StringSplitOptions.None));
    }

    /// <summary>
    /// Splits a string based on tab characters to a ReadOnlySpan of JsonEncodedText.
    /// </summary>
    /// <returns><![CDATA[ReadOnlySpan<JsonEncodedText>]]></returns>
    public ReadOnlySpan<JsonEncodedText> ReadLineTabJsonStrings(string value)
    {
      return new ReadOnlySpan<JsonEncodedText>(ReadLineTabStrings(value).ToArray().Select(x => JsonEncodedText.Encode(x)).ToArray());
    }

    public IEnumerable<Memory<string>> Utf8ReadLinesToMemory(Stream input)
    {
      using StreamReader reader = new StreamReader(input, Encoding.UTF8);
      while (!reader.EndOfStream)
      {
        yield return reader.ReadLine().Split("\t", StringSplitOptions.None).AsMemory();
      }
    }

    //public static async Task ReadLinesAsync()
    //{
    //  var jsonFile = new FileInfo("US2.json");
    //  using var fs = File.Open("US.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
    //  using StreamReader reader = new StreamReader(fs, Encoding.UTF8);

    //  var i = 0;


    //  using var writefs = jsonFile.OpenWrite();
    //  using var writer = new Utf8JsonWriter(writefs);
    //  writer.WriteStartArray();
    //  while (!reader.EndOfStream)
    //  {
    //    var mem = (await reader.ReadLineAsync()).Split("\t", StringSplitOptions.None).AsMemory();
    //    decimal lat;
    //    decimal lon;
    //    var hasLat = decimal.TryParse(mem.Span[^3], out lat);
    //    var hasLon = decimal.TryParse(mem.Span[^2], out lon);

    //    writer.WriteStartObject();
    //    writer.WriteNumber("PostalCode", int.Parse(mem.Span[1]));
    //    writer.WriteString("City", mem.Span[2]);
    //    writer.WriteString("State", mem.Span[4]);
    //    writer.WriteString("County", mem.Span[5]);
    //    if (hasLat) writer.WriteNumber("Lat", lat);
    //    if (hasLon) writer.WriteNumber("Long", lon);
    //    writer.WriteEndObject();
    //  }
    //  writer.WriteEndArray();
    //  // await writer.FlushAsync();
    //  await writer.DisposeAsync();
    //  while (writer.BytesPending > 0)
    //  {
    //    System.Threading.Thread.Sleep(5);
    //  }
    //  jsonFile.Refresh();

    //}


  }
}
