using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

    public abstract bool IsLeaf { get; }

    public abstract IEnumerable<NodeBase> GetChildren();

    public IEnumerable<NodeBase> FindLeafNodes(Func<NodeBase, bool> finder)
    {
      return GetChildren().Descendants(i => i.GetChildren()).Where(x=> x.IsLeaf).Where(finder);
    }

    public IEnumerable<NodeBase> FindCompositeNodes(Func<NodeBase, bool> finder)
    {
      return GetChildren().Descendants(i => i.GetChildren()).Where(x => !x.IsLeaf).Where(finder);
    }

    protected static readonly IReadOnlyCollection<NodeBase> Dummy;

    static NodeBase()
    {
      var dummyList = new List<NodeBase>();
      Dummy = new ReadOnlyCollection<NodeBase>(dummyList);
    }
  }

  public interface ICompositeNode
  {
    void Add(NodeBase item);
    void Remove(NodeBase item);
    int Count();
    //IEnumerable<NodeBase> GetChildren();

    //IEnumerable<NodeBase> Find(Func<NodeBase, bool> finder);
  }

  public class CompositeNode : NodeBase , ICompositeNode
  {
    private readonly IList<NodeBase> _items;
    private readonly IReadOnlyCollection<NodeBase> _children;

    public CompositeNode(string name)
    {
      Name = name;
      _items = new Collection<NodeBase>();
      _children = new ReadOnlyCollection<NodeBase>(_items);
    }

    public void Add(NodeBase item)
    {
      _items.Add(item);
    }

    public void Remove(NodeBase item)
    {
      _items.Remove(item);
    }

    public int Count()
    {
      return _items.Count;
    }

    public override bool IsLeaf
    {
      get { return false; }
    }

    public override IEnumerable<NodeBase> GetChildren()
    {
      return _children;
    }
  }


  public class NodeItem : NodeBase
  {
    private readonly string _path;

    public NodeItem(string path, string name, int id)
    {
      _path = path;
      Name = name;
      Id = id;
    }

    public override bool IsLeaf
    {
      get { return true; }
    }

    public override IEnumerable<NodeBase> GetChildren()
    {
      return Dummy;
    }
  }


}
