using AbstractFactoryLogic.Enums;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryLogic.Logic
{
  /// <summary>
  /// A static factory method creator class that extends how we are using our factory pattern.
  /// </summary>
  public static class PassengerCreator
  {
    /// <summary> The static method to return an IAstronaut with a specified gravity value. </summary>
    public static IAstronaut GetAstronaut(GravityType gravity)
    {
      switch (gravity)
      {
        case GravityType.Weightless:
          return new WeightlessPassengerFactory().NewAstronaut();
        default:
          return new PassengerFactory().NewAstronaut();
      }
    }

    /// <summary> The static method to return an ICosmonaut with a specified gravity value. </summary>
    public static ICosmonaut GetCosmonaut(GravityType gravity)
    {
      switch (gravity)
      {
        case GravityType.Weightless:
          return new WeightlessPassengerFactory().NewCosmonaut();
        default:
          return new PassengerFactory().NewCosmonaut();
      }
    }

    /// <summary> The static method to return an IToy with a specified gravity value. </summary>
    public static IToy GetToy(GravityType gravity)
    {
      switch (gravity)
      {
        case GravityType.Weightless:
          return new WeightlessPassengerFactory().NewToy();
        default:
          return new PassengerFactory().NewToy();
      }
    }

    /// <summary>
    /// The public static method to create a new IPassenger of a specified type
    /// corresponding to the enumeration of PassengerType provided with the
    /// desired gravity enumeration applied.
    /// </summary>
    public static IPassenger GetPassenger(PassengerType passengerType, GravityType gravity)
    {
      switch (passengerType)
      {
        case PassengerType.Cosmonaut:
          return GetCosmonaut(gravity);
        case PassengerType.Toy:
          return GetToy(gravity);
        default:
          return GetAstronaut(gravity);
      }
    }
  }
}
