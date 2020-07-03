namespace decorate.Models
{
  /// <summary>
  /// The CustomSticker Dynamic Decorator Composition Sample that
  /// adds some custom logic over a normal Sticker object.
  /// </summary>
  public class CustomSticker : Sticker
  {
    private readonly Sticker m_Sticker;

    // The parameterless constructor is only needed by program.cs since we are
    // calling this with the ModuleDecorator. You could otherwise skip this.

    /// <summary> A parameterless CustomSticker constructor. ONLY NEEDED FOR EXAMPLE, COULD BE SKIPPED OTHERWISE. </summary>
    public CustomSticker()
    {
      m_Sticker = new Sticker
      {
        Location = "bumper",
        StickerShape = "rectangle"
      };
    }

    /// <summary> A constructor that takes a Sticker that will be decorated and an additional sticker type string. </summary>
    public CustomSticker(Sticker sticker, string stickerType)
    {
      m_Sticker = sticker;
      StickerType = stickerType;
    }

    /// <summary> Decorates the sticker logic with a sticker type string. </summary>
    public string StickerType { get; set; }

    /// <summary> Overriden method to apply our decoration. </summary>
    public override string Apply()
    {
      return string.Format("Applying {0} {1} sticker to the {2}!", m_Sticker.StickerShape, StickerType, m_Sticker.Location);
    }
  }
}
