namespace LogApi.Models
{
  public class BackgroundServiceStateResult
  {
    public BackgroundServiceSignalType RequestType { get; set; }

    public bool IsEnabled { get; set; }
  }

  public enum BackgroundServiceSignalType
  {
    Disable,
    Enable,
    Status
  }
}
