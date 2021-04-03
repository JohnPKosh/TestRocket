using System.Collections.Generic;
using Newtonsoft.Json;

namespace CompositeLibrary.Infrastructure.GoF
{
  /// <summary>
  /// The public interface allowing for the
  /// adding and removing of children
  /// </summary>
  public interface IComposite
  {
    /// <summary> The public method to add a child </summary>
    void AddChild(Component c);

    /// <summary> The public method to remove a child </summary>
    void RemoveChild(Component c);
  }

  /// <summary>
  /// The base public abstract component class that
  /// composites and leaf class inherit from.
  /// </summary>
  public abstract class Component
  {
    /// <summary>
    /// The public abstract base class for composite objects
    /// </summary>
    public Component(string name) => Name = name;

    /// <summary>
    /// The public name property of the component
    /// </summary>
    [JsonProperty(Order = 1)]
    public string Name { get; protected set; }
  }

  /// <summary>
  /// The public concrete composite class (+ children)
  /// </summary>
  public class Composite : Component, IComposite
  {
    /// <summary> The default Composite constructor </summary>
    public Composite(string name) : base(name) { }

    /// <summary> The Children of a composite</summary>
    [JsonProperty(
      Order = 2,
      ReferenceLoopHandling = ReferenceLoopHandling.Serialize
    )]
    public List<Component> Children { get; set; }
      = new List<Component>();

    #region IComposite Interface Implementations

    /// <summary> The public method to add a child </summary>
    public void AddChild(Component component)
    {
      Children.Add(component);
    }

    /// <summary> The public method to remove a child </summary>
    public void RemoveChild(Component component)
    {
      Children.Remove(component);
    }

    #endregion
  }

  /// <summary>
  /// The public concrete leaf class (no children)
  /// </summary>
  public class Leaf : Component
  {
    public Leaf(string name) : base(name) { }
  }
}
