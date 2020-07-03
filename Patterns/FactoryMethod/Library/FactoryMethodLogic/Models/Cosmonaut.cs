using FactoryMethodLogic.Common;
using FactoryMethodLogic.Logic;
using FactoryMethodLogic.Models.Interfaces;

namespace FactoryMethodLogic.Models
{
  /// <summary>
  /// The IPassengerFactory defers NewPassenger creation to the CosmonautFactory
  /// so we will create it below near the concrete model.
  /// There is no need for seperate code file in this case.
  /// </summary>
  public class CosmonautFactory : IPassengerFactory
  {
    protected override IPassenger CreatePassenger()
    {
      return new Cosmonaut();
    }
  }

  /// <summary>
  /// This is the Cosmonaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Cosmonaut : IPassenger
  {
    /// <summary> Say some clever phrase here. </summary>
    public string Speak()
    {
      return FactoryConstants.CSM_SPK;
    }

    /// <summary> Begin launch command instruction. </summary>
    public string LaunchCommand()
    {
      return FactoryConstants.CSM_LAUNCH;
    }
  }
}
