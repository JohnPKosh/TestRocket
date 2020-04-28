using System;
using AbstractFactoryLogic.Logic;
using AbstractFactoryLogic.Enums;
using AbstractFactoryLogic.Models.Interfaces;

namespace abstractfactory
{
  class Program
  {
    static void Main(string[] args)
    {
      hr();
      con("Houston we have a problem...");
      hr();

      /* Using specific raw abstract factory pattern example */
      var earthFactory = new PassengerFactory(); // normal gravity
      executeBasicExample(earthFactory);
      hr();

      var spaceFactory = new WeightlessPassengerFactory(); // weightless
      executeBasicExample(spaceFactory);
      hr();

      /* Using gravity type and static creator logic */
      executeGravityCreator(GravityType.Normal); // normal gravity
      hr();

      executeGravityCreator(GravityType.Weightless); // weightless
      hr();

      /* Do some fancy construction here */
      con("[Incoming Astronaut Transmissions]:");
      executeComplexLaunchCommand(PassengerType.Astronaut, GravityType.Normal);
      executeComplexLaunchCommand(PassengerType.Astronaut, GravityType.Weightless);
      hr();
      con("[Incoming Cosmonaut Transmissions]:");
      executeComplexLaunchCommand(PassengerType.Cosmonaut, GravityType.Normal);
      executeComplexLaunchCommand(PassengerType.Cosmonaut, GravityType.Weightless);
      hr();
      con("[Incoming Toy Transmissions]:");
      executeComplexLaunchCommand(PassengerType.Toy, GravityType.Normal);
      executeComplexLaunchCommand(PassengerType.Toy, GravityType.Weightless);
      hr();

      con("Thanks for flying with us!");
    }

    /// <summary>
    /// The basic abstract factory approach.
    /// </summary>
    private static void executeBasicExample(IPassengerFactory earthFactory)
    {
      // Create our passengers using the basic abstract factory methods.
      var astronaut = earthFactory.NewAstronaut();
      var cosmonaut = earthFactory.NewCosmonaut();
      var toy = earthFactory.NewToy();

      // push buttons
      con(astronaut.Speak());

      // flip switch
      con(cosmonaut.Speak());

      // pull string
      con(toy.Speak());
    }

    /// <summary>
    /// A slightly easier approach.
    /// </summary>
    private static void executeGravityCreator(GravityType gravity)
    {
      var astronaut = PassengerCreator.GetAstronaut(gravity);
      var cosmonaut = PassengerCreator.GetCosmonaut(gravity);
      var toy = PassengerCreator.GetToy(gravity);

      // push buttons
      con(astronaut.PushButton());

      // flip switch
      con(cosmonaut.FlipSwitch());

      // pull string
      con(toy.PullString());
    }

    /// <summary>
    /// A more complex approach where are passengers have different things they can do.
    /// </summary>
    private static void executeComplexLaunchCommand(PassengerType passengerType, GravityType gravity)
    {
      var passenger = PassengerCreator.GetPassenger(passengerType, gravity);
      if(passenger is IAstronaut astronaut)
      {
        con(astronaut.LaunchCommand());
        con(astronaut.PushButton()); // Astronauts push buttons
      }
      else if (passenger is ICosmonaut cosmonaut)
      {
        con(cosmonaut.LaunchCommand());
        con(cosmonaut.FlipSwitch()); // Cosmonaut flips switches
      }
      else if (passenger is IToy toy)
      {
        con(toy.LaunchCommand());
        con(toy.PullString()); // Pull Toy's string
      }
    }

    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
  }
}
