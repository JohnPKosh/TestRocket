using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RocketWriter
{
  public class TabReader
  {
    public const string TAB_HEADER_SIMPLE = "country code	postal code	place name	admin name1	admin code1	admin name2	admin code2	admin name3	admin code3	latitude	longitude	accuracy";

    public const string TAB_LINE_SIMPLE = "US	99591	Saint George Island	Alaska	AK	Aleutians West (CA)	016			56.5944	-169.6186	1";

    /// <summary>
    /// Splits a string based on tab characters to a ReadOnlySpan of strings.
    /// TODO: Review actual usefullness of this since there are likely more efficient
    /// ways to do this byte or stream buffer reading. May apply for header row?
    /// </summary>
    /// <returns><![CDATA[ReadOnlySpan<string>]]></returns>
    public ReadOnlySpan<string> GetTabStrings(string value = TAB_LINE_SIMPLE)
    {
      return new ReadOnlySpan<string>(value.Split('\t', StringSplitOptions.None));
    }

    /// <summary>
    /// Splits a string based on tab characters to a ReadOnlySpan of JsonEncodedText.
    /// </summary>
    /// <returns><![CDATA[ReadOnlySpan<JsonEncodedText>]]></returns>
    public ReadOnlySpan<JsonEncodedText> GetTabJsonStrings(string value = TAB_LINE_SIMPLE)
    {
      return new ReadOnlySpan<JsonEncodedText>(GetTabStrings(value).ToArray().Select(x => JsonEncodedText.Encode(x)).ToArray());
    }
  }
}
