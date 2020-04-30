using System;
using FactoryMethodLogic.Enums;
using FactoryMethodLogic.Logic;

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

      hr();
      con("Prepare to Launch!");
      hr();

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

      //Console.ReadKey(true);
    }

    private static void prepareForLaunch(PassengerType type)
    {
      var passenger = PassengerCreator.Create(type);
      con(passenger.Speak());
      con(passenger.LaunchCommand());
      hr();
    }

    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);

  }
}
