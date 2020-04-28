using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryLogic.Models
{
  /// <summary>
  /// This is the Toy concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Toy : IToy
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

    /// <summary>Pull his strings.</summary>
    public string PullString()
    {
      return FactoryConstants.TOY_PULL_STR;
    }
  }

  /// <summary>
  /// This is the Weightless Toy concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessToy : IToy
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return FactoryConstants.TOY_SPK_ZERO_G;
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return FactoryConstants.TOY_LAUNCH_ZERO_G;
    }

    /// <summary>Pull his strings.</summary>
    public string PullString()
    {
      return FactoryConstants.TOY_PULL_STR_ZERO_G;
    }
  }
}
