using factorymethod.Enums;
using factorymethod.Models;
using factorymethod.Models.Interfaces;

namespace factorymethod.Logic
{
  /// <summary>
  /// A static creator class that extends how we are using our factory method pattern.
  /// </summary>
  public static class PassengerCreator
  {
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
    }
  }
}
