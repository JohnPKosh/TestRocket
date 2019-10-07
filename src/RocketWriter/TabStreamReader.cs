using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RocketWriter
{
  public class TabStreamReader
  {
    private const string STRING_SEPERATOR = "\t";

    private Stream m_Stream { get; set; }

    public TabStreamReader(Stream input)
    {
      m_Stream = input;
    }


    public IEnumerable<Memory<string>> Utf8ReadLinesToMemory()
    {
      using StreamReader reader = new StreamReader(m_Stream, Encoding.UTF8);
      while (!reader.EndOfStream)
      {
        yield return reader.ReadLine().Split(STRING_SEPERATOR, StringSplitOptions.None).AsMemory();
      }
    }

    public IEnumerable<Memory<JsonEncodedText>> Utf8ReadLinesToJsonMemory()
    {
      using StreamReader reader = new StreamReader(m_Stream, Encoding.UTF8);
      while (!reader.EndOfStream)
      {
        yield return reader.ReadLine().Split(STRING_SEPERATOR, StringSplitOptions.None).Select(x => JsonEncodedText.Encode(x)).ToArray().AsMemory();
      }
    }

    public ReadOnlySpan<JsonEncodedText> StringsToReadOnlyJsonEncodedTextSpan(IEnumerable<string> value)
    {
      return new ReadOnlySpan<JsonEncodedText>(value.ToArray().Select(x => JsonEncodedText.Encode(x)).ToArray());
    }

    public void WriteToSimpleJsonStream(IEnumerable<string> propertyNames, Stream outputStream)
    {
      WriteToSimpleJsonStream(StringsToReadOnlyJsonEncodedTextSpan(propertyNames), outputStream);
    }

    public void WriteToSimpleJsonStream(ReadOnlySpan<JsonEncodedText> propertyNames, Stream outputStream)
    {
      using var jw = new Utf8JsonWriter(outputStream);
      jw.WriteStartArray();
      foreach (var line in Utf8ReadLinesToJsonMemory())
      {
        jw.WriteStartObject();
        for (int i = 0; i < propertyNames.Length; i++)
        {
          if (i >= line.Length) break;
          var m_Value = line.Span[i];
          if(!string.IsNullOrWhiteSpace(m_Value.ToString()))jw.WriteString(propertyNames[i], line.Span[i]);
        }
        jw.WriteEndObject();
      }
      jw.WriteEndArray();

      jw.Dispose(); // Clean Up
      while (jw.BytesPending > 0)
      {
        System.Threading.Thread.Sleep(5);
      }
    }

  }
}
