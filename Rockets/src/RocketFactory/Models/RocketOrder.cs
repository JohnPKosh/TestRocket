namespace Factory.Models
{
  /* Using new C# Language feature here */
#nullable enable

  /// <summary>
  /// Needed some way to complicate some simple data enough by applying some business order logic.
  /// </summary>
  public class RocketOrder
  {
    public int OrderId { get; set; }
    public string? Customer { get; set; }
    public BallisticRocket? BallisticRocket { get; set; }
    public Spaceship? Spaceship { get; set; }
    public CargoRocket? CarrierRocket { get; set; }
  }
#nullable disable
}