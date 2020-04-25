using System;
using abstractfactory.Logic;
using abstractfactory.Enums;
using abstractfactory.Models.Interfaces;

namespace abstractfactory
{
  class Program
  {
    static void Main(string[] args)
    {
      AddBreak();
      ConOut("Houston we have a problem...");
      AddBreak();

      /* Using specific raw abstract factory pattern example */
      var earthFactory = new PassengerFactory(); // normal gravity
      RawPatternExample(earthFactory);
      AddBreak();

      var spaceFactory = new WeightlessPassengerFactory(); // weightless
      RawPatternExample(spaceFactory);
      AddBreak();

      /* Using gravity type and static creator logic */
      GravityCreator(GravityType.Normal); // normal gravity
      AddBreak();

      GravityCreator(GravityType.Weightless); // weightless
      AddBreak();

      /* Do some fancy construction here */
      ComplexLaunchCommand(PassengerType.Astronaut, GravityType.Normal);
      ComplexLaunchCommand(PassengerType.Astronaut, GravityType.Weightless);
      ComplexLaunchCommand(PassengerType.Cosmonaut, GravityType.Normal);
      ComplexLaunchCommand(PassengerType.Cosmonaut, GravityType.Weightless);
      ComplexLaunchCommand(PassengerType.Toy, GravityType.Normal);
      ComplexLaunchCommand(PassengerType.Toy, GravityType.Weightless);
      AddBreak();

      ConOut("Thanks for flying with us!");
    }

    /// <summary>
    /// The basic abstract factory approach.
    /// </summary>
    private static void RawPatternExample(IPassengerFactory earthFactory)
    {
      // Create our passengers using the basic abstract factory methods.
      var astronaut = earthFactory.NewAstronaut();
      var cosmonaut = earthFactory.NewCosmonaut();
      var toy = earthFactory.NewToy();

      // push buttons
      astronaut.Speak();

      // flip switch
      cosmonaut.Speak();

      // pull string
      toy.Speak();
    }

    /// <summary>
    /// A slightly easier approach.
    /// </summary>
    private static void GravityCreator(GravityType gravity)
    {
      var astronaut = PassengerCreator.GetAstronaut(gravity);
      var cosmonaut = PassengerCreator.GetCosmonaut(gravity);
      var toy = PassengerCreator.GetToy(gravity);

      // push buttons
      astronaut.PushButton();

      // flip switch
      cosmonaut.FlipSwitch();

      // pull string
      toy.PullString();
    }

    /// <summary>
    /// A more complex approach where are passengers have different things they can do.
    /// </summary>
    private static void ComplexLaunchCommand(PassengerType passengerType, GravityType gravity)
    {
      var passenger = PassengerCreator.GetPassenger(passengerType, gravity);
      if(passenger is IAstronaut astronaut)
      {
        astronaut.LaunchCommand();
        astronaut.PushButton(); // Astronauts push buttons
      }
      else if (passenger is ICosmonaut cosmonaut)
      {
        cosmonaut.LaunchCommand();
        cosmonaut.FlipSwitch(); // Cosmonaut flips switches
      }
      else if (passenger is IToy toy)
      {
        toy.LaunchCommand();
        toy.PullString(); // Pull Toy's string
      }
    }

    // Helpers to make things easier to read above.

    private static void AddBreak() => Console.WriteLine("\n**********************************\n");
    private static void ConOut(string text) => Console.WriteLine($"\n{text}\n");
  }
}
