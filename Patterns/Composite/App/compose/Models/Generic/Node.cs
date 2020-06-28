using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace compose.Models.Generic
{
  public class Node<T> : IEnumerable<Node<T>>
  {
    #region Constructors and Class Initialization

    public Node() { }

    public Node(T value)
    {
      Value = value;
    }

    #endregion

    #region Fields and Props

    public T Value { get; set; } = default;

    public bool IsLeaf { get; set; } = false; // TODO: determine how to address this

    public List<Node<T>> In = new List<Node<T>>();

    public List<Node<T>> Out = new List<Node<T>>();

    #endregion

    public void CreateNewChild(T value)
    {
      this.ConnectTo(new Node<T>() { Value = value });
    }

    public void CreateNewChildren(IEnumerable<T> values) =>
      this.ConnectTo(values.Select(x => new Node<T>() { Value = x }));

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


  public static class NodeExtensions
  {
    #region IEnumerable Scoped Methods

    public static void ConnectTo<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      foreach (var from in self)
        foreach (var to in child)
        {
          from.Out.Add(to);
          to.In.Add(from);
        }
    }

    public static void Disconnect<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> child)
    {
      if (ReferenceEquals(self, child)) return;

      foreach (var from in self)
        foreach (var to in child)
        {
          from.Out.Remove(to);
          to.In.Remove(from);
        }
    }

    public static void ReParent<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> newParent, IEnumerable<Node<T>> child)
    {
      self.Disconnect(child);
      newParent.ConnectTo(child);
    }

    #endregion


    #region Single Scoped Node Methods

    private static void DisconnectChildren<T>(this Node<T> self, IEnumerable<Node<T>> children)
    {
      if (ReferenceEquals(self, children)) return;

      foreach (var to in children)
      {
        self.Out.Remove(to);
        to.In.Remove(self);
      }
    }

    private static void ReParentChild<T>(this Node<T> self, Node<T> newParent, IEnumerable<Node<T>> child)
    {
      self.DisconnectChildren(child);
      newParent.ConnectTo(child);
    }

    public static void ReParentChildren<T>(this Node<T> self, Node<T> newParent)
    {
      if (ReferenceEquals(self, newParent)) return;

      var children = self.Out.ToList();
      foreach (var o in children)
      {
        o.In.First(x => x.Equals(self)).ReParentChild(newParent, o);
      }
    }

    public static void ReParentChildren<T>(this Node<T> self, Node<T> newParent, Func<Node<T>, bool> predicate)
    {
      if (ReferenceEquals(self, newParent)) return;

      var children = self.Out.Where(predicate).ToList();
      foreach (var o in children)
      {
        o.In.First(x => x.Equals(self)).ReParentChild(newParent, o);
      }
    }

    #endregion

  }

}
