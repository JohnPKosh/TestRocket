using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryLogic.Models
{
  /// <summary>
  /// This is the Cosmonaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Cosmonaut : ICosmonaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return FactoryConstants.CSM_SPK;
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return FactoryConstants.CSM_LAUNCH;
    }

    /// <summary>Flip out cosmonaut.</summary>
    public string FlipSwitch()
    {
      return FactoryConstants.CSM_FLIP;
    }
  }

  /// <summary>
  /// This is the Weightless Cosmonaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessCosmonaut : ICosmonaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return FactoryConstants.CSM_SPK_ZERO_G;
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return FactoryConstants.CSM_LAUNCH_ZERO_G;
    }

    /// <summary>Flip out cosmonaut.</summary>
    public string FlipSwitch()
    {
      return FactoryConstants.CSM_FLIP_ZERO_G;
    }
  }


}
