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
    public void Speak()
    {
      Console.WriteLine("Where is my Tang?");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("Git-r-Done!");
    }

    /// <summary>Push astronaut's button.</summary>
    public void PushButton()
    {
      Console.WriteLine("Why did you push that?");
    }
  }

  /// <summary>
  /// This is the Weightless Astronaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessAstronaut : IAstronaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public void Speak()
    {
      Console.WriteLine("Look at me I am floating!");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("Up up and away!");
    }

    /// <summary>Push astronaut's button.</summary>
    public void PushButton()
    {
      Console.WriteLine("Look at the blinky lights!");
    }
  }
}
