namespace decorate.Models
{
  /// <summary>
  /// The ModuleDecorator is a Static Decorator implementation
  /// that will decorate an object based on the generic type argument
  /// passed in to the constructor.
  /// </summary>
  /// <example>
  ///   var controlPanelDecorator = new ModuleDecorator<Sticker>("control panel");
  ///   con(controlPanelDecorator.Apply());
  /// </example>
  public class ModuleDecorator<T> : Decorator where T : Decorator, new()
  {
    private const string DEFAULT_LOCATION = "capsule";

    /// <summary>A new private readonly Decorator of T created on instantiation.</summary>
    private readonly T m_Decoration = new T();

    /// <summary>A default constructor that applies the DEFAULT_LOCATION</summary>
    public ModuleDecorator() : this(DEFAULT_LOCATION) { }

    /// <summary>A constructor that accepts a location argument to be applied on the Decorator base class property.</summary>
    public ModuleDecorator(string location)
    {
      Location = location;
    }

    /// <summary>The overridden Apply method of the base abstract method.</summary>
    public override string Apply()
    {
      // The below is fairly bloated logic, normally you would want to externalize
      // this logic into seperate code components (private methods etc.)
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
