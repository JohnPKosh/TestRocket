using FactoryMethodLogic.Enums;
using FactoryMethodLogic.Models;
using FactoryMethodLogic.Models.Interfaces;

namespace FactoryMethodLogic.Logic
{
  /// <summary>
  /// A static factory method creator class that extends how we are using our factory pattern.
  /// </summary>
  public static class PassengerCreator
  {
    /// <summary>
    /// The public static method to create a new IPassenger of a specified type
    /// corresponding to the enumeration of PassengerType provided.
    /// </summary>
    public static IPassenger Create(PassengerType type)
    {
      switch (type)
      {
        case PassengerType.Astronaut:
          return new AstronautFactory().NewPassenger();
        case PassengerType.Cosmonaut:
          return new CosmonautFactory().NewPassenger();
        case PassengerType.Toy:
          return new ToyFactory().NewPassenger();
        default:
          return new AstronautFactory().NewPassenger();
      }

      // Could also be done in C# 8+ like so:
      /*
      return type switch
      {
        PassengerType.Astronaut => new AstronautFactory().NewPassenger(),
        PassengerType.Cosmonaut => new CosmonautFactory().NewPassenger(),
        PassengerType.Toy => new ToyFactory().NewPassenger(),
        _ => new AstronautFactory().NewPassenger(),
      };
      */
    }
  }
}
