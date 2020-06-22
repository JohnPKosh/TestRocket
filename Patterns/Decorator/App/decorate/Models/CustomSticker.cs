namespace decorate.Models
{
  public class CustomSticker : Sticker
  {
    private readonly Sticker m_Sticker;

    // The parameterless constructor is only needed by program.cs since we are
    // calling this with the ModuleDecorator. You could otherwise skip this.
    public CustomSticker()
    {
      m_Sticker = new Sticker();
      m_Sticker.Location = "bumper";
      m_Sticker.StickerShape = "rectangle";
    }

    public CustomSticker(Sticker sticker, string stickerType)
    {
      m_Sticker = sticker;
      StickerType = stickerType;
    }

    public string StickerType { get; set; }

    public override string Apply()
    {
      return string.Format("Applying {0} {1} sticker to the {2}!", m_Sticker.StickerShape, StickerType, m_Sticker.Location);
    }
  }
}
