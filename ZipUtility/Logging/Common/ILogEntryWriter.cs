using SRF.FileLogging.Models;

namespace SRF.FileLogging.Common
{
  public interface ILogEntryWriter
  {
    string GenerateLogHeaderLine();

    string GenerateLogEntryLine(LogEntry Info);
  }
}
