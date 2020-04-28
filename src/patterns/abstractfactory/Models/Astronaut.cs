using System;
using abstractfactory.Models.Interfaces;

namespace abstractfactory.Models
{

  /// <summary>
  /// This is the Astronaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Astronaut : IAstronaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return "Where is my Tang?";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return "Git-r-Done!";
    }

    /// <summary>Push astronaut's button.</summary>
    public string PushButton()
    {
      return "Why did you push that?";
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
      return "Look at me I am floating!";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return "Up up and away!";
    }

    /// <summary>Push astronaut's button.</summary>
    public string PushButton()
    {
      return "Look at the blinky lights!";
    }
  }
}
