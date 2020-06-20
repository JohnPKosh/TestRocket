using ProxyLogic.Common;
using ProxyLogic.Logic;
using ProxyLogic.Models.Interfaces;

namespace ProxyLogic.Models
{
  /// <summary>
  /// The IPassengerFactory defers NewPassenger creation to the ToyFactory
  /// so we will create it below near the concrete model.
  /// There is no need for seperate code file in this case.
  /// </summary>
  public class ToyFactory : IPassengerFactory
  {
    protected override IPassenger CreatePassenger()
    {
      return new Toy();
    }
  }

  /// <summary>
  /// This is the Toy concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Toy : IPassenger
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return FactoryConstants.TOY_SPK;
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return FactoryConstants.TOY_LAUNCH;
    }
  }
}
