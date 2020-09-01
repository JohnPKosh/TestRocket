using System.Collections.Generic;

namespace SRF.FileLogging.Models
{
  /// <summary>
  /// Scope information regarding a log entry
  /// </summary>
  public class LogScopeInfo
  {
    /// <summary>
    /// Used when the Scope is just a string type, else it is null.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Used when the Scope is a Dictionary-like object
    /// </summary>
    public Dictionary<string, object> Properties { get; set; }
  }
}
