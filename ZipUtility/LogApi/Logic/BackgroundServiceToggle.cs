namespace LogApi.Logic
{
  public class BackgroundServiceToggle : IBackgroundServiceToggle
  {
    public bool IsEnabled { get; private set; } = true;

    public void Disable()
    {
      IsEnabled = false;
    }

    public void Enable()
    {
      IsEnabled = true;
    }
  }
}
