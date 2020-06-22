using System;
using System.Collections.Generic;
using System.Text;

namespace decorate.Models
{
  public class ModuleDecorator<T> : Decorator where T : Decorator, new()
  {
    private readonly T m_Decoration = new T();

    public ModuleDecorator() : this("capsule") { }

    public ModuleDecorator(string location)
    {
      Location = location;
    }

    public override string Apply()
    {
      if (m_Decoration is Paint)
      {
        var paint = m_Decoration as Paint;
        paint.Location = Location;
        paint.Color = "pthalo green";
        return paint.Apply();
      }
      else if (m_Decoration is CustomSticker)
      {
        var sticker = m_Decoration as CustomSticker;
        sticker.Location = Location;
        sticker.StickerShape = "square";
        sticker.StickerType = "cling";
        return sticker.Apply();
      }
      else if (m_Decoration is Sticker)
      {
        var sticker = m_Decoration as Sticker;
        sticker.Location = Location;
        sticker.StickerShape = "rectangle";
        return sticker.Apply();
      }
      else
      {
        m_Decoration.Location = Location;
        return m_Decoration.Apply();
      }
    }
  }
}
