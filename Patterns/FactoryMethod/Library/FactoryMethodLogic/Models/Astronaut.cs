using FactoryMethodLogic.Common;
using FactoryMethodLogic.Logic;
using FactoryMethodLogic.Models.Interfaces;

namespace FactoryMethodLogic.Models
{
  /// <summary>
  /// The IPassengerFactory defers NewPassenger creation to the AstronautFactory
  /// so we will create it below near the concrete model.
  /// There is no need for seperate code file in this case.
  /// </summary>
  public class AstronautFactory : IPassengerFactory
  {
    protected override IPassenger CreatePassenger()
    {
      return new Astronaut();
    }
  }

  /// <summary>
  /// This is the Astronaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Astronaut : IPassenger
  {
    /// <summary> Say some clever phrase here. </summary>
    public string Speak()
    {
      return FactoryConstants.AST_SPK;
    }

    /// <summary> Begin launch command instruction. </summary>
    public string LaunchCommand()
    {
      return FactoryConstants.AST_LAUNCH;
    }
  }
}
