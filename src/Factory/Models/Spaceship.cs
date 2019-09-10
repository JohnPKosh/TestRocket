namespace Factory.Models
{
  public class Spaceship : IRocket
  {
    public Spaceship()
    {
      Height = 68;
    }
    public int Height { get; set; }
  }
}