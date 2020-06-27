using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace compose.Models.Media
{
  static public class NodeExtensions
  {
    static public IEnumerable<T> Descendants<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> DescendBy)
    {
      foreach (T value in source)
      {
        yield return value;

        foreach (T child in DescendBy(value).Descendants<T>(DescendBy))
        {
          yield return child;
        }
      }
    }
  }

  public abstract class NodeBase
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; protected set; }

    public abstract void Add(NodeBase item); // leaf only
    public abstract void Remove(NodeBase item); // leaf only
    public abstract int Count();  // leaf only child count
    public abstract bool IsLeaf { get; } // true when leaf false when not
    public abstract IEnumerable<NodeBase> GetChildren();   // true when leaf

    public IEnumerable<NodeBase> Find(Func<NodeBase, bool> finder)
    {
      return GetChildren().Descendants(i => i.GetChildren()).Where(finder);
    }

    protected static readonly IReadOnlyCollection<NodeBase> Dummy;

    static NodeBase()
    {
      var dummyList = new List<NodeBase>();
      Dummy = new ReadOnlyCollection<NodeBase>(dummyList);
    }
  }

  public class NodeLeaf : NodeBase
  {
    private readonly IList<NodeBase> _items;
    private readonly IReadOnlyCollection<NodeBase> _children;

    public NodeLeaf(string title)
    {
      Name = title;
      _items = new Collection<NodeBase>();
      _children = new ReadOnlyCollection<NodeBase>(_items);
    }

    public override void Add(NodeBase item)
    {
      _items.Add(item);
    }

    public override void Remove(NodeBase item)
    {
      _items.Remove(item);
    }

    public override int Count()
    {
      return _items.Count;
    }

    public override bool IsLeaf
    {
      get { return true; }
    }

    public override IEnumerable<NodeBase> GetChildren()
    {
      return _children;
    }
  }


  public class NodeItem : NodeBase
  {
    private readonly string _path;

    public NodeItem(string path, string name)
    {
      _path = path;
      Name = name;
    }

    public override void Add(NodeBase item)
    {
      throw new NotImplementedException();
    }

    public override void Remove(NodeBase item)
    {
      throw new NotImplementedException();
    }

    public override int Count()
    {
      throw new NotImplementedException();
    }

    public override bool IsLeaf
    {
      get { return false; }
    }

    public override IEnumerable<NodeBase> GetChildren()
    {
      return Dummy;
    }
  }


}
