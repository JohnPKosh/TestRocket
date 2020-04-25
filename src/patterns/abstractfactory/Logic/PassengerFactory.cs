using abstractfactory.Enums;
using abstractfactory.Models;
using abstractfactory.Models.Interfaces;

namespace abstractfactory.Logic
{
  /// <summary>
  /// A concrete passenger factory class.
  /// </summary>
  public class PassengerFactory : IPassengerFactory
  {
    public IAstronaut NewAstronaut()
    {
      return new Astronaut();
    }

    public ICosmonaut NewCosmonaut()
    {
      return new Cosmonaut();
    }

    public IToy NewToy()
    {
      return new Toy();
    }
  }

  /// <summary>
  /// A concrete weightless passenger factory class.
  /// </summary>
  public class WeightlessPassengerFactory : IPassengerFactory
  {
    public IAstronaut NewAstronaut()
    {
      return new WeightlessAstronaut();
    }

    public ICosmonaut NewCosmonaut()
    {
      return new WeightlessCosmonaut();
    }

    public IToy NewToy()
    {
      return new WeightlessToy();
    }
  }
}
