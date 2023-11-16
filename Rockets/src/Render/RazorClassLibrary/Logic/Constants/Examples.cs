using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Logic.Constants;

public static class HTML
{
  public const string START_TAG_PREFIX = "<";
  public const string START_TAG_SUFFIX = ">";

  public const string END_TAG_PREFIX = "</";
  public const string END_TAG_SUFFIX = ">";
}

public static class CSHARP
{
  public const string BODY_PREFIX = "{";
  public const string BODY_SUFFFIX = "}";

  public const string ACCESS_PUBLIC = "public";
  public const string ACCESS_PROTECTED = "protected";
  public const string ACCESS_INTERNAL = "internal";
  public const string ACCESS_PROTECTED_INTERNAL = "protected internal";
  public const string ACCESS_PRIVATE = "private";
  public const string ACCESS_PRIVATE_PROTECTED = "private protected";
  public const string ACCESS_FILE = "file";

  public const string PARTIAL_MODIFIER = "partial";

  public const string MODIFIER_ABSTRACT = "abstract";
  public const string MODIFIER_VIRTUAL = "virtual";
  public const string MODIFIER_STATIC = "static";

  public const string ATTRIBUTE_PREFIX = ":";
  public const string ATTRIBUTE_SEPERATOR = ",";

  public const string TYPE_PARAMETER_PREFIX = "<";
  public const string TYPE_PARAMETER_SEPERATOR = ",";
  public const string TYPE_PARAMETER_SUFFIX = ">";

  public const string TYPE_CONSTRAINT_PREFIX = "where";
  public const string TYPE_CONSTRAINT_SEPERATOR = ":";
}


/*

public: Access isn't restricted.
protected: Access is limited to the containing class or types derived from the containing class.
internal: Access is limited to the current assembly.
protected internal: Access is limited to the current assembly or types derived from the containing class.
private: Access is limited to the containing type.
private protected: Access is limited to the containing class or types derived from the containing class within the current assembly.
file: The declared type is only visible in the current source file. File scoped types are generally used for source generators.

*/