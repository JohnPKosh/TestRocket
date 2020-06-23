namespace decorate.Models
{
  /// <summary>
  /// The Sticker class extends the underlying abstract Decorator class
  /// thereby applying the decorator pattern to it's base class.
  /// </summary>
  public class Sticker : Decorator
  {
    /// <summary>We override the abstract Apply method with our custom decoration method.</summary>
    public override string Apply()
    {
      return string.Format("Applying {0} sticker to the {1}!", StickerShape, Location);
    }

    /// <summary>Represents a new extended decorated property describing the shape of a sticker.</summary>
    public string StickerShape { get; set; }
  }
}
