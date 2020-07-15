using ProxyLogic.Logic;
using ProxyLogic.Models.Interfaces;

namespace ProxyLogic.Models
{
  /// <summary>
  /// The IPassengerFactory defers NewPassenger creation to the ToyFactory
  /// so we will create it below near the concrete model.
  /// There is no need for seperate code file in this case.
  /// </summary>
  public class RemoteControlFactory : IPassengerFactory
  {
    protected override IPassenger CreatePassenger()
    {
      return new RemoteControlToy();
    }
  }

  /// <summary>
  /// This is the RemoteControlToy concrete model implementation of the IPassenger interface. **This is a Remote Proxy sample**
  /// </summary>
  public class RemoteControlToy : IPassenger
  {
    private RemoteController m_remoteController = new RemoteController(); // Imagine this is a remote controller operating a remote control toy.

    /// <summary> Begin launch command instruction. </summary>
    public string LaunchCommand() // Here we implement the IPassenger method to launch
    {
      return m_remoteController.SendCommand(); // This is not implementing IPassenger. It is a remote system and has it's own logic.
    }

    /// <summary> Say some clever phrase here. </summary>
    public string Speak() // Here we implement the IPassenger method to speak
    {
      return m_remoteController.Babble(); // This is not implementing IPassenger. It is a remote system and has it's own logic.
    }
  }
}
