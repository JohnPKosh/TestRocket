namespace LogApi.Models
{
  /// <summary>
  /// Encapsulates the DTO result of a background service request call.
  /// </summary>
  public class BackgroundServiceStateResult
  {
    /// <summary>
    /// The signal request type sent in the request.
    /// </summary>
    public BackgroundServiceSignalType RequestType { get; set; }

    /// <summary>
    /// The result of the request indicating if the host background service is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }
  }

  /// <summary>
  /// Enum indicating what type of signal request is being processed.
  /// </summary>
  public enum BackgroundServiceSignalType
  {
    Disable,
    Enable,
    Status
  }
}
