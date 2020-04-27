using System;
using factorymethod.Logic;
using factorymethod.Enums;

namespace factorymethod
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
       * Because preparing for launch at bottom was so ugly,
       * we just add some PassengerCreator and a private method
       * so as not to be repetitive
      */

      prepareForLaunch(PassengerType.Astronaut);
      prepareForLaunch(PassengerType.Cosmonaut);
      prepareForLaunch(PassengerType.Toy);

      /*
      Far too ugly, but the below exemplifies the factory method pattern...

      IPassengerFactory cf = new CosmonautFactory();
      var cosmonaut = cf.NewPassenger();
      cosmonaut.Speak();
      cosmonaut.LaunchCommand();

      IPassengerFactory af = new AstronautFactory();
      var astronaut = af.NewPassenger();
      astronaut.Speak();
      astronaut.LaunchCommand();

      IPassengerFactory tf = new ToyFactory();
      var toy = tf.NewPassenger();
      toy.Speak();
      toy.LaunchCommand();
      */

      Console.ReadKey(true);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    private static void prepareForLaunch(PassengerType type)
    {
      var passenger = PassengerCreator.Create(type);
      passenger.Speak();
      passenger.LaunchCommand();
    }
  }
}
