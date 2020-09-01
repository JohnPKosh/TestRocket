using System;
using SRF.FileLogging.Models;

namespace SRF.FileLogging.Common
{
  /// <summary>
  /// The public interface to encapsulate LogEntry transformation and persistence methods.
  /// </summary>
  public interface ILogEntryWriter
  {
    /// <summary>
    /// Generates a header line for file persisted log sinks.
    /// </summary>
    ReadOnlySpan<char> GenerateLogHeaderLine();

    /// <summary>
    /// Generates a Log Entry converted to a string line.
    /// </summary>
    ReadOnlySpan<char> GenerateLogEntryLine(LogEntry Info);
  }
}
