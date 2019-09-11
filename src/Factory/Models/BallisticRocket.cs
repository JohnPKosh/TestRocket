namespace Factory.Models
{
  /// <summary>
  /// A very dangerous rocket that has a measureable Maximum Velocity default.
  /// </summary>
  public class BallisticRocket : IRocket
  {
    public BallisticRocket()
    {
      Height = 55;
    }
    public int Height { get; set; }

    public decimal MaximumVelocity()
    {
      return 50000M;
    }

    // default implementations for PayloadCapacity and Explodes
  }
}