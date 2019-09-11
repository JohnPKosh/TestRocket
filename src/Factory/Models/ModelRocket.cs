namespace Factory.Models
{
  /// <summary>
  /// Model Rockets are just toy rockets with limited payload capacity
  /// </summary>
  public class ModelRocket : IRocket
  {
    public ModelRocket()
    {
      Height = 5;
    }
    public int Height { get; set; }

    public int? PayloadCapacity => 3;

    // default interface implementations for Explodes and MaximumVelocity    
  }
}