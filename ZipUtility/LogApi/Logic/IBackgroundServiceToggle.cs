namespace LogApi.Logic
{
  /// <summary>
  /// Interface to interupt processing of a low level hosted
  /// BackgroundService accessed using the WorkerBase abstract class.
  /// </summary>
  public interface IBackgroundServiceToggle
  {
    /// <summary>
    /// The property indicating if the background service is enabled or not.
    /// </summary>
    bool IsEnabled { get; }

    /// <summary>Disables or interupts execution within a concrete WorkerBase implemented class.</summary>
    void Disable();

    /// <summary>Enables execution within a concrete WorkerBase implemented class.</summary>
    void Enable();
  }
}
