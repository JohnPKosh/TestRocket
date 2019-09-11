namespace Factory.Models
{
  /// <summary>
  /// Spaceships are rockets used by martians who experiment on unwary humans.
  /// </summary>
  public class Spaceship : IRocket
  {
    public Spaceship()
    {
      Height = 68;
      Explodes = false;
    }
    public int Height { get; set; }

    public bool? Explodes { get; set; }
    
    // default interface implementations for PayloadCapacity, Explodes and MaximumVelocity 
  }
}