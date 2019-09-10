namespace Factory.Models
{
  public class CarrierRocket : IRocket
  {
    public CarrierRocket()
    {
      Height = 92;
    }
    public int Height { get; set; }

    public int? PayloadCapacity => 21250;
  }
}