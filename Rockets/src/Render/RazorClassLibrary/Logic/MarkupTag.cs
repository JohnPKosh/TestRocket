using System.Text;

using Microsoft.AspNetCore.Components;

namespace RazorClassLibrary.Logic;

public struct MarkupTag
{
  public MarkupTag(string name, string content, bool isHtmlEncoded = false, IDictionary<string, string>? parameters = null)
  {
    Name = name;
    Content = [content];
    IsHtmlEncoded = isHtmlEncoded;
    Parameters = parameters;
  }

  public MarkupTag(string name, object[] content, bool isHtmlEncoded = false, IDictionary<string, string>? parameters = null)
  {
    Name = name;
    Content = content;
    IsHtmlEncoded = isHtmlEncoded;
    Parameters = parameters;
  }

  public string Name { get; set; }

  public object[] Content { get; set; }

  public bool IsHtmlEncoded { get; set; }

  public IDictionary<string, string>? Parameters { get; set; }

  public override readonly string ToString()
    => HtmlTagGenerator.GetTag(Name, GetContentString(Content), false, Parameters);

  /// <summary>
  /// Casts a <see cref="MarkupTag"/> to a <see cref="MarkupString"/>.
  /// </summary>
  /// <param name="value">The <see cref="string"/> value.</param>
  public static explicit operator MarkupString(MarkupTag value)
      => new(value.ToString());


  private static string? GetContentString(object[] objects)
  {
    if (objects == null) return null;
    var sb = new StringBuilder();
    foreach (var c in objects)
    {
      sb.Append(c.ToString());
    }
    return sb.ToString();
  }

}