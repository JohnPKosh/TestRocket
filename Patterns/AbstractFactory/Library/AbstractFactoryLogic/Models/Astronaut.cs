using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryLogic.Models
{
  /// <summary>
  /// This is the Astronaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Astronaut : IAstronaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return FactoryConstants.AST_SPK;
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return FactoryConstants.AST_LAUNCH;
    }

    /// <summary>Push astronaut's button.</summary>
    public string PushButton()
    {
      return FactoryConstants.AST_PUSH_BTN;
    }
  }

  /// <summary>
  /// This is the Weightless Astronaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessAstronaut : IAstronaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return FactoryConstants.AST_SPK_ZERO_G;
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return FactoryConstants.AST_LAUNCH_ZERO_G;
    }

    /// <summary>Push astronaut's button.</summary>
    public string PushButton()
    {
      return FactoryConstants.AST_PUSH_BTN_ZERO_G;
    }
  }
}
