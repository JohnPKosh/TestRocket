namespace Factory.Models
{
  /// <summary>
  /// Cargo rockets deliver satellites into orbit or Tang to ISS.
  /// </summary>
  public class CargoRocket : IRocket
  {
    public CargoRocket()
    {
      Height = 92;
    }
    public int Height { get; set; }

    public int? PayloadCapacity => 21250;

    // default interface implementations for Explodes and MaximumVelocity 
  }
}