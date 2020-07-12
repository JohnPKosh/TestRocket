using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace compose.Models.Generic
{
  public abstract class Node<T> : IEnumerable<Node<T>>
  {
    #region Constructors and Class Initialization

    public Node() { }

    public Node(T value, NodeMeta meta = null)
    {
      Value = value;
      if(meta != null) Meta = meta;
    }

    #endregion

    #region Fields and Props

    public T Value { get; set; } = default;

    public abstract bool IsLeaf { get; protected set; }

    public NodeMeta Meta { get; set; } = new NodeMeta();

    public List<Node<T>> In = new List<Node<T>>();

    public List<Node<T>> Out = new List<Node<T>>();

    #endregion

    #region Create New Child Methods

    public virtual void CreateNewLeaf(T value, NodeMeta meta = null) =>
      this.AddChildren(new LeafNode<T>() { Value = value, Meta = meta });

    public virtual void CreateNewLeaves(IEnumerable<T> values) =>
      this.AddChildren(values.Select(x => new LeafNode<T>() { Value = x }));

    public virtual void CreateNewComposite(T value, NodeMeta meta = null) =>
      this.AddChildren(new CompositeNode<T>() { Value = value, Meta = meta });

    public virtual void CreateNewComposites(IEnumerable<T> values) =>
      this.AddChildren(values.Select(x => new CompositeNode<T>() { Value = x }));

    #endregion

    #region Get Descendents Methods

    public IEnumerable<Node<T>> GetDescendents()
    {
      return this.Descendants(i => i.Out);
    }

    public IEnumerable<Node<T>> Descendants(Func<Node<T>, IEnumerable<Node<T>>> descendBy)
    {
      foreach (Node<T> value in this)
      {
        yield return value;

        foreach (Node<T> child in descendBy(value))
        {
          foreach (var c in child.Descendants(descendBy))
          {
            yield return c;
          }
        }
      }
    }

    public IEnumerable<Node<T>> FindNodes(Func<Node<T>, bool> finder)
    {
      return this.Descendants(i => i.Out).Where(finder);
    }

    public IEnumerable<Node<T>> FindLeafNodes(Func<Node<T>, bool> finder)
    {
      return this.Descendants(i => i.Out).Where(x => x.IsLeaf).Where(finder);
    }

    public IEnumerable<Node<T>> FindCompositeNodes(Func<Node<T>, bool> finder)
    {
      return this.Descendants(i => i.Out).Where(x => !x.IsLeaf).Where(finder);
    }

    #endregion

    #region IEnumerable Implementation

    public IEnumerator<Node<T>> GetEnumerator()
    {
      yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion
  }

  public class NodeMeta
  {
    public string DisplayName { get; set; }

    public string Name { get; set; }

    public string GroupName { get; set; }

    public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
  }

  public static class NodeExtensions
  {
    #region IEnumerable Scoped Methods

    public static void AddChildren<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      foreach (var from in self)
        foreach (var to in child)
        {
          from.Out.Add(to);
          to.In.Add(from);
        }
    }

    public static void RemoveChildren<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      foreach (var from in self)
        foreach (var to in child)
        {
          from.Out.Remove(to);
          to.In.Remove(from);
        }
    }

    public static void ReParentChildren<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> newParent, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      self.RemoveChildren(child);
      newParent.AddChildren(child);
    }

    public static void ReParentChildren<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> newParent)
    {
      if (ReferenceEquals(self, newParent)) return;

      foreach (var from in self)
      {
        var children = from.Out.ToList();
        foreach (var o in children)
        {
          o.In.First(x => x.Equals(from)).ReParentChildren(newParent, o);
        }
      }
    }

    public static void ReParentChildrenWhere<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> newParent, Func<Node<T>, bool> predicate)
    {
      if (ReferenceEquals(self, newParent)) return;

      foreach (var from in self)
      {
        var children = from.Out.Where(predicate).ToList();
        foreach (var o in children)
        {
          o.In.First(x => x.Equals(from)).ReParentChildren(newParent, o);
        }
      }
    }

    #endregion

    #region Single Scoped Node Methods

    public static void AddChildren<T>(this Node<T> self, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      foreach (var to in child)
      {
        self.Out.Add(to);
        to.In.Add(self);
      }
    }

    private static void RemoveChildren<T>(this Node<T> self, IEnumerable<Node<T>> children)
    {
      if (ReferenceEquals(self, children)) return;

      foreach (var to in children)
      {
        self.Out.Remove(to);
        to.In.Remove(self);
      }
    }

    private static void ReParentChildren<T>(this Node<T> self, Node<T> newParent, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      self.RemoveChildren(child);
      newParent.AddChildren(child);
    }

    public static void ReParentChildren<T>(this Node<T> self, Node<T> newParent)
    {
      if (ReferenceEquals(self, newParent)) return;

      var children = self.Out.ToList();
      foreach (var o in children)
      {
        o.In.First(x => x.Equals(self)).ReParentChildren(newParent, o);
      }
    }

    public static void ReParentChildrenWhere<T>(this Node<T> self, Node<T> newParent, Func<Node<T>, bool> predicate)
    {
      if (ReferenceEquals(self, newParent)) return;

      var children = self.Out.Where(predicate).ToList();
      foreach (var o in children)
      {
        o.In.First(x => x.Equals(self)).ReParentChildren(newParent, o);
      }
    }

    #endregion

  }

}
