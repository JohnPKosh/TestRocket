namespace decorate.Models
{
  /// <summary>
  /// The Paint class extends the underlying abstract Decorator class
  /// thereby applying the decorator pattern to it's base class.
  /// </summary>
  public class Paint : Decorator
  {
    /// <summary>We override the abstract Apply method with our custom decoration method.</summary>
    public override string Apply()
    {
      return string.Format("Giving the {0} a little {1} color!", Location, Color);
    }

    /// <summary>Represents a new extended decorated property describing the color of a Paint.</summary>
    public string Color { get; set; }
  }
}
