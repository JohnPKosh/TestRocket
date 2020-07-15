namespace AbstractFactoryLogic.Models.Interfaces
{
  /// <summary>
  /// An interface used for passenger models functionality
  /// </summary>
  public interface IPassenger
  {
    /// <summary> Say some clever phrase here. </summary>
    string Speak();

    /// <summary> Begin launch command instruction. </summary>
    string LaunchCommand();
  }

  /// <summary>
  /// Represents astronaut specific interface
  /// </summary>
  public interface IAstronaut : IPassenger
  {
    /// <summary> Push astronaut's button. </summary>
    string PushButton();
  }

  /// <summary>
  /// Represents cosmonaut specific interface
  /// </summary>
  public interface ICosmonaut : IPassenger
  {
    /// <summary> Flip out cosmonaut. </summary>
    string FlipSwitch();
  }

  /// <summary>
  /// Represents toy specific interface
  /// </summary>
  public interface IToy : IPassenger
  {
    /// <summary> Pull his strings. </summary>
    string PullString();
  }
}
