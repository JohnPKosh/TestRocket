using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryLogic.Logic
{
  /// <summary>
  /// The abstract factory pattern lets a class defer instantiation to subclasses.
  /// The following interface is acting like a factory of factories.</summary>
  public interface IPassengerFactory
  {
    /// <summary>
    /// *** This is the public method used to create a new Astronaut ***
    /// </summary>
    IAstronaut NewAstronaut();

    /// <summary>
    /// *** This is the public method used to create a new Cosmonaut ***
    /// </summary>
    ICosmonaut NewCosmonaut();

    /// <summary>
    /// *** This is the public method used to create a new Toy ***
    /// </summary>
    IToy NewToy();
  }
}
