using AbstractFactoryLogic.Models;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryLogic.Logic
{
  /// <summary>
  /// A concrete passenger factory class.
  /// </summary>
  /// <remarks> Notice we used standard method syntax compared to weightless factory. </remarks>
  public class PassengerFactory : IPassengerFactory
  {
    /// <summary> *** This is the public method used to create a new Astronaut *** </summary>
    public IAstronaut NewAstronaut()
    {
      return new Astronaut();
    }

    /// <summary> *** This is the public method used to create a new Cosmonaut *** </summary>
    public ICosmonaut NewCosmonaut()
    {
      return new Cosmonaut();
    }

    /// <summary> *** This is the public method used to create a new Toy *** </summary>
    public IToy NewToy()
    {
      return new Toy();
    }
  }

  /// <summary>
  /// A concrete weightless passenger factory class.
  /// </summary>
  /// <remarks> Notice we simplified the implemented methods below with expression bodies. </remarks>
  public class WeightlessPassengerFactory : IPassengerFactory
  {
    /// <summary> *** This is the public method used to create a new Astronaut *** </summary>
    public IAstronaut NewAstronaut() => new WeightlessAstronaut();

    /// <summary> *** This is the public method used to create a new Cosmonaut *** </summary>
    public ICosmonaut NewCosmonaut() => new WeightlessCosmonaut();

    /// <summary> *** This is the public method used to create a new Toy *** </summary>
    public IToy NewToy() => new WeightlessToy();
  }
}
