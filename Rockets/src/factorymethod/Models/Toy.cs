using System;
using factorymethod.Logic;
using factorymethod.Models.Interfaces;

namespace factorymethod.Models
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
    public void Speak()
    {
      Console.WriteLine("Hello I am Buzz");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("To infinity and beyond!");
    }
  }
}
