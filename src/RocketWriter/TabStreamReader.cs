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
        yield return reader.ReadLine().Split("\t", StringSplitOptions.None).AsMemory();
      }
    }

    public IEnumerable<Memory<JsonEncodedText>> Utf8ReadLinesToJsonMemory()
    {
      using StreamReader reader = new StreamReader(m_Stream, Encoding.UTF8);
      while (!reader.EndOfStream)
      {
        yield return reader.ReadLine().Split("\t", StringSplitOptions.None).Select(x => JsonEncodedText.Encode(x)).ToArray().AsMemory();
      }
    }

    public ReadOnlySpan<string> ReadLineTabStrings(string value)
    {
      return new ReadOnlySpan<string>(value.Split('\t', StringSplitOptions.None));
    }

  }
}
