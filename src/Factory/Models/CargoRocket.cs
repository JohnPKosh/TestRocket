namespace Factory.Models
{
  public class CargoRocket : IRocket
  {
    public CargoRocket()
    {
      Height = 92;
    }
    public int Height { get; set; }

    public int? PayloadCapacity => 21250;
  }
}