namespace factorymethod.Models.Interfaces
{
  /// <summary>
  /// An interface used for passenger models functionality
  /// </summary>
  public interface IPassenger
  {
    /// <summary>Say some clever phrase here.</summary>
    void Speak();

    /// <summary>Begin launch command instruction.</summary>
    void LaunchCommand();
  }
}
