using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using RazorClassLibrary.Logic.Constants;

namespace RazorClassLibrary.Logic;

public static class HtmlTagGenerator
{

  public static string GetTag(string tagName, string? content, bool encode = true, IDictionary<string, string>? parameters = null)
    => GetTag(tagName, () => content ?? string.Empty, encode, parameters);

  public static string GetTag(string tagName, Func<string> content, bool encode, IDictionary<string, string>? parameters = null)
  => $"{getStartTag(tagName, parameters)}{(encode ? HtmlEncoder.Default.Encode(content.Invoke()) : content.Invoke())}{getEndTag(tagName)}";

  private static string getStartTag(string tagName, IDictionary<string, string>? parameters = null)
    => parameters != null && parameters.Any() ?
    $"{HTML.START_TAG_PREFIX} {getParameterString(parameters)}{tagName}{HTML.START_TAG_SUFFIX}":
    $"{HTML.START_TAG_PREFIX}{tagName}{HTML.START_TAG_SUFFIX}";

  private static string getEndTag(string tagName) => $"{HTML.END_TAG_PREFIX}{tagName}{HTML.END_TAG_SUFFIX}";

  private static string getParameterString(IDictionary<string, string> parameters, string quotedIdentifier = "'")
    => string.Join(' ', parameters.Select(x => $"{x.Key}={quotedIdentifier}{x.Value}{quotedIdentifier}"));
}
