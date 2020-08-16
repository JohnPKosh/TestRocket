namespace LogApi.Logic
{
  public interface IBackgroundServiceToggle
  {
    bool IsEnabled { get; }
    void Disable();
    void Enable();
  }
}
