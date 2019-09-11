namespace Factory.Models
{
  /// <summary>
  /// Represents a Bottle Rocket type that explodes by default.
  /// </summary>
  public class BottleRocket : IRocket
  {
    public BottleRocket()
    {
      Height = 1;
    }
    public int Height { get; set; }

    public bool? Explodes { get; set; }

    // default interface implementations for PayloadCapacity and MaximumVelocity
  }
}