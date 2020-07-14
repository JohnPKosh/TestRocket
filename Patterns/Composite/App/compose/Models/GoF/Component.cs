using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace compose.Models.GoF
{
  public interface IComponent
  {
    string Name { get; }
    void RecurseTree();
  }

  public interface IComposite: IComponent
  {
    void AddChild(Component c);
    void RemoveChild(Component c);
  }

  /// <summary>
  /// The 'Component' abstract class
  /// </summary>

  public abstract class Component : IComponent
  {
    protected const char LVL_CHAR = '-';

    public Component(string name) => Name = name;

    [JsonProperty(Order = 1)]
    public string Name { get; protected set; }

    public abstract void RecurseTree();

    protected internal abstract void RecurseTree(int depth);
  }

  /// <summary>
  /// The 'Composite' class
  /// </summary>
  public class Composite : Component, IComposite
  {
    [JsonProperty(Order = 2, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
    public List<Component> Children { get; set; } = new List<Component>();


    public Composite(string name) : base(name) { }

    public void AddChild(Component component)
    {
      Children.Add(component);
    }

    public void RemoveChild(Component component)
    {
      Children.Remove(component);
    }

    public override void RecurseTree() => RecurseTree(1);

    protected internal override void RecurseTree(int depth)
    {
      Console.WriteLine(new string(LVL_CHAR, depth) + Name);
      foreach (Component component in Children)
      {
        component.RecurseTree(depth + 2);
      }
    }
  }

  /// <summary>
  /// The 'Leaf' class
  /// </summary>
  public class Leaf : Component, IComponent
  {
    // Constructor
    public Leaf(string name) : base(name) { }

    public override void RecurseTree() => RecurseTree(1);

    protected internal override void RecurseTree(int depth)
    {
      Console.WriteLine(new string(LVL_CHAR, depth) + Name);
    }
  }
}
