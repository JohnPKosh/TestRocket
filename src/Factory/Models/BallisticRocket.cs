namespace Factory.Models
{
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
  }
}