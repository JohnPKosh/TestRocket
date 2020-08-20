namespace LogApi.Models
{
  /// <summary>
  /// Encapsulates a background service signal request used to enable or disable the host service.
  /// </summary>
  public class BackgroundServiceRequest
  {
    public BackgroundServiceSignalType RequestType { get; set; }
  }
}
