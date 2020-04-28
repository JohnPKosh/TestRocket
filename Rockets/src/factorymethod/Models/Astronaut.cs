using System;
using factorymethod.Logic;
using factorymethod.Models.Interfaces;

namespace factorymethod.Models
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
  }
}
