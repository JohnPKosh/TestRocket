using ProxyLogic.Enums;
using ProxyLogic.Models;
using ProxyLogic.Models.Interfaces;

namespace ProxyLogic.Logic
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
        case PassengerType.RemoteControlToy:
          return new RemoteControlFactory().NewPassenger();
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
