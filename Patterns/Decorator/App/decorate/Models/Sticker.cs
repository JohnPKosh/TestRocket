namespace decorate.Models
{
  public class Sticker : Decorator
  {
    public override string Apply()
    {
      return string.Format("Applying {0} sticker to the {1}!", StickerShape, Location);
    }

    public string StickerShape { get; set; }
  }
}
