using abstractfactory.Enums;
using abstractfactory.Models.Interfaces;

namespace abstractfactory.Logic
{
  public static class PassengerCreator
  {
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
