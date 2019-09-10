namespace Factory.Models
{
/* Using new C# Language feature here */
#nullable enable
  public class RocketOrder
  {
    public int OrderId { get; set; }
    public string? Customer { get; set; }
    public BallisticRocket? BallisticRocket { get; set; }
    public Spaceship? Spaceship { get; set; }
    public CarrierRocket? CarrierRocket { get; set; }
  }
#nullable disable
}