using System;
using System.Collections.Generic;

namespace compose.Models.GoF
{
  /// <summary>
  /// The 'Component' abstract class
  /// </summary>

  public abstract class Component
  {
    protected const char LVL_CHAR = '-';

    public Component(string name) => Name = name;

    public string Name { get; protected set; }

    public abstract void AddChild(Component c);

    public abstract void RemoveChild(Component c);

    public abstract void RecurseTree();

    protected internal abstract void RecurseTree(int depth);
  }

  /// <summary>
  /// The 'Composite' class
  /// </summary>
  public class Composite : Component
  {
    public List<Component> Children { get; set; } = new List<Component>();

    // Constructor
    public Composite(string name) : base(name) { }

    public override void AddChild(Component component)
    {
      Children.Add(component);
    }

    public override void RemoveChild(Component component)
    {
      Children.Remove(component);
    }

    public override void RecurseTree() => RecurseTree(1);

    protected internal override void RecurseTree(int depth)
    {
      Console.WriteLine(new string(LVL_CHAR, depth) + Name);
      foreach (var component in Children)
      {
        component.RecurseTree(depth + 2);
      }
    }
  }

  /// <summary>
  /// The 'Leaf' class
  /// </summary>
  public class Leaf : Component
  {
    // Constructor
    public Leaf(string name) : base(name) { }

    public override void AddChild(Component c)
    {
      Console.WriteLine("You should not be able to add to a leaf");
    }

    public override void RemoveChild(Component c)
    {
      Console.WriteLine("You should not be able to remove from a leaf");
    }

    public override void RecurseTree() => RecurseTree(1);

    protected internal override void RecurseTree(int depth)
    {
      Console.WriteLine(new string(LVL_CHAR, depth) + Name);
    }
  }
}
