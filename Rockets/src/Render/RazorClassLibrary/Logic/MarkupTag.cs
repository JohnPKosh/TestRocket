using System.IO;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Xml;
using System.Xml.Linq;

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
    => HtmlTagGenerator.GetTag(Name, GetContentString(Content), false /* Fix encode logic */, Parameters);

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
      if (c != null) sb.Append(c is string s ? HtmlEncoder.Default.Encode(s) : c.ToString());
    }
    return sb.ToString();
  }

}

public static partial class ExtensionMethods
{

  //public static string ToPrettyXml(this string str, XmlWriterSettings settings = null)
  //{
  //  if (string.IsNullOrWhiteSpace(str)) return str;
  //  try
  //  {
  //    //XmlDocument xml = new XmlDocument();
  //    //xml.LoadXml(str);
  //    var xml = XElement.Parse(str);

  //    if (settings == null)
  //    {
  //      // Modify these settings to format the XML as desired
  //      settings = new XmlWriterSettings
  //      {
  //        OmitXmlDeclaration = true,
  //        Indent = true,
  //        NewLineOnAttributes = true,
  //        Encoding = Encoding.UTF8,
  //        NewLineHandling = NewLineHandling.Entitize,
  //      };
  //    }

  //    using (StringWriter sw = new StringWriter())
  //    {
  //      using (var textWriter = XmlWriter.Create(sw, settings))
  //      {
  //        xml.Save(textWriter);
  //      }
  //      sw.Flush();
  //      return sw.ToString();
  //    }
  //  }
  //  catch { }
  //  return str;
  //}


  //public static string FormatXml(this string xml)
  //{
  //  var tags = xml
  //      .Split('<')
  //      .Select(tag => tag.TrimEnd().EndsWith(">") ? tag.TrimEnd() : tag); //Trim whitespace between tags, but not at the end of values

  //  var previousTag = tags.First(); //Preserve content before the first tag, e.g. if the initial < is missing
  //  var formattedXml = new StringBuilder(previousTag);
  //  var indention = 0;

  //  foreach (var tag in tags.Skip(1))
  //  {
  //    if (previousTag.EndsWith(">"))
  //    {
  //      formattedXml.AppendLine();
  //      if (tag.StartsWith("/"))
  //      {
  //        indention = Math.Max(indention - 1, 0);
  //        formattedXml.Append(new string('\t', indention));
  //      }
  //      else
  //      {
  //        formattedXml.Append(new string('\t', indention));
  //        if (!tag.EndsWith("/>"))
  //        {
  //          indention++;
  //        }
  //      }
  //    }
  //    else
  //    {
  //      indention = Math.Max(indention - 1, 0);
  //    }

  //    formattedXml.Append("<");
  //    formattedXml.Append(tag);
  //    previousTag = tag;
  //  }

  //  return formattedXml.ToString();
  //}


  /// <summary>
  /// this function creates an indented format of html with new lines for all elements and text.  errors result in the original text being returned.
  /// </summary>
  public static string FormatHtml(this string content)
  {
    var original = content;
    var open = "<";
    var slash = "/";
    var close = ">";

    var depth = 0; // the indentation
    var adjustment = 0; //adjustment to depth, done after writing text

    var o = 0; // open      <   index of this character
    var c = 0; // close     >   index of this character
    var s = 0; // slash     /   index of this character
    var n = 0; // next      where to start looking for characters in the next iteration
    var b = 0; // begin     resolved start of usable text
    var e = 0; // end       resolved   end of usable test

    string snippet;

    try
    {
      using (var writer = new StringWriter())
      {
        while (b > -1 && n > -1)
        {
          o = content.IndexOf(open, n);
          s = content.IndexOf(slash, n);
          c = content.IndexOf(close, n);
          adjustment = 0;

          b = n; // begin where we left off in the last iteration
          if (o > -1 && o < c && o == n)
          {
            // starts with "<tag>text"
            e = c; // end at the next closing tag
            adjustment = 2; //for after this node
          }
          else
          {
            // starts with "text<tag>"
            e = o - 1; // end at the next opening tag
          }

          if (b == o && b + 1 == s) // ?is the 2nd character a slash, this the a closing tag: </div>
          {
            depth -= 2;//adjust immediately, not afterward ...for closing tag
            adjustment = 0;
          }

          if ((s + 1) == c && c == e) // don't adjust depth for singletons:  <br/>
          {
            adjustment = 0;
          }

          var length = (e - b + 1);
          if (length < 0)
          {
            snippet = content.Substring(b); // happens on the final iteration
          }
          else
          {
            snippet = content.Substring(b, (e - b + 1));
          }

          if (snippet == "<br>" || snippet == "<hr>") // don't adjust depth for singletons which lack slashes: <br>
          {
            adjustment = 0;
          }

          //write the text
          if (!string.IsNullOrEmpty(snippet.Trim()))
          {
            if (depth > 0) writer.Write(new string(' ', depth)); // add the indentation
            writer.Write(snippet);
            writer.Write(Environment.NewLine);
          }

          depth += adjustment; //adjust for the next line which is likely nested

          n = e + 1; // the next iteration start at the end of this one.
        }

        return writer.ToString().TrimEnd();
      }
    }
    catch /*(Exception ex)*/
    {
      //log("unable to format html. " + ex.message);
      return original;
    }
  }

}
