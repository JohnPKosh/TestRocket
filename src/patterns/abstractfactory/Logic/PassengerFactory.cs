using abstractfactory.Models;
using abstractfactory.Models.Interfaces;

namespace abstractfactory.Logic
{
  /// <summary>
  /// A concrete passenger factory class.
  /// </summary>
  public class PassengerFactory : IPassengerFactory
  {
    /// <summary>
    /// *** This is the public method used to create a new Astronaut ***
    /// </summary>
    public IAstronaut NewAstronaut()
    {
      return new Astronaut();
    }

    /// <summary>
    /// *** This is the public method used to create a new Cosmonaut ***
    /// </summary>
    public ICosmonaut NewCosmonaut()
    {
      return new Cosmonaut();
    }

    /// <summary>
    /// *** This is the public method used to create a new Toy ***
    /// </summary>
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
    /// <summary>
    /// *** This is the public method used to create a new Astronaut ***
    /// </summary>
    public IAstronaut NewAstronaut()
    {
      return new WeightlessAstronaut();
    }

    /// <summary>
    /// *** This is the public method used to create a new Cosmonaut ***
    /// </summary>
    public ICosmonaut NewCosmonaut()
    {
      return new WeightlessCosmonaut();
    }

    /// <summary>
    /// *** This is the public method used to create a new Toy ***
    /// </summary>
    public IToy NewToy()
    {
      return new WeightlessToy();
    }
  }
}
