namespace FactoryMethodLogic.Models.Interfaces
{
  /// <summary>
  /// An interface used for passenger models functionality
  /// </summary>
  public interface IPassenger
  {
    /// <summary>Say some clever phrase here.</summary>
    string Speak();

    /// <summary>Begin launch command instruction.</summary>
    string LaunchCommand();
  }
}
