namespace LogApi.Logic
{
  /// <summary>
  /// A basic concrete implementation of IBackgroundServiceToggle used to
  /// interupt processing of a low level hosted BackgroundService accessed
  /// using the WorkerBase abstract class.
  /// </summary>
  public class BackgroundServiceToggle : IBackgroundServiceToggle
  {
    /// <summary>
    /// The public property indicating if the background service is enabled or not.
    /// (default is true to enable hard application/service reboots to automatically start)
    /// </summary>
    public bool IsEnabled { get; private set; } = true;

    /// <summary>Disables or interupts execution within a concrete WorkerBase implemented class.</summary>
    public void Disable()
    {
      IsEnabled = false;
    }

    /// <summary>Enables execution within a concrete WorkerBase implemented class.</summary>
    public void Enable()
    {
      IsEnabled = true;
    }
  }
}
