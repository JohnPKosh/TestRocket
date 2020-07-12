using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace compose.Models.Generic
{
  public abstract class Node<T> : IEnumerable<Node<T>>
  {
    public const string ADD_SELF_MSG = "You cannot add a node to itself!";
    public const string REMOVE_SELF_MSG = "You cannot remove a node from itself!";
    public const string REPARENT_SELF_MSG = "You cannot reparent a node to itself!";

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

    public List<Node<T>> Parents = new List<Node<T>>();

    public List<Node<T>> Children = new List<Node<T>>();

    public bool IsRootNode => !Parents.Any();

    #endregion

    #region Basic Public Methods

    public virtual void ReParent(IEnumerable<Node<T>> newParents)
    {
      if (newParents == null) throw new ArgumentNullException(nameof(newParents));
      if (ReferenceEquals(this, newParents)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParents));

      Parents.Clear();
      ((Node<T>)newParents).AddChildren(this);
    }

    public virtual void AddChildren(IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      if (ReferenceEquals(this, childNodes)) throw new ArgumentException(ADD_SELF_MSG, nameof(childNodes));

      foreach (var self in this)
        foreach (var child in childNodes)
        {
          self.Children.Add(child);
          child.Parents.Add(self);
        }
    }

    public virtual void RemoveChildren(IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      if (ReferenceEquals(this, childNodes)) throw new ArgumentException(REMOVE_SELF_MSG, nameof(childNodes));

      foreach (var self in this)
        foreach (var child in childNodes)
        {
          self.Children.Remove(child);
          child.Parents.Remove(self);
        }
    }

    public virtual void ReParentChildren(IEnumerable<Node<T>> newParents, IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      if (newParents == null) throw new ArgumentNullException(nameof(newParents));
      if (ReferenceEquals(this, newParents)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParents));

      RemoveChildren(childNodes);

      foreach (var self in newParents)
        foreach (var child in childNodes)
        {
          self.Children.Add(child);
          child.Parents.Add(self);
        }
    }

    public virtual void ReParentChildren(IEnumerable<Node<T>> newParents)
    {
      if (newParents == null) throw new ArgumentNullException(nameof(newParents));
      if (ReferenceEquals(this, newParents)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParents));

      foreach (var self in this)
      {
        var children = self.Children.ToList();
        foreach (var child in children)
        {
          child.Parents.First(x => x.Equals(self)).ReParentChildren(newParents, child);
        }
      }
    }

    public virtual void ReParentChildrenWhere(IEnumerable<Node<T>> newParents, Func<Node<T>, bool> predicate)
    {
      if (predicate == null) throw new ArgumentNullException(nameof(predicate));
      if (newParents == null) throw new ArgumentNullException(nameof(newParents));
      if (ReferenceEquals(this, newParents)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParents));

      foreach (var self in this)
      {
        var children = self.Children.Where(predicate).ToList();
        foreach (var child in children)
        {
          child.Parents.First(x => x.Equals(self)).ReParentChildren(newParents, child);
        }
      }
    }

    #endregion

    #region Create New Child Methods

    public virtual void CreateNewLeaf(T value, NodeMeta meta = null) =>
      AddChildren(new LeafNode<T>() { Value = value, Meta = meta });

    public virtual void CreateNewLeaves(IEnumerable<T> values) =>
      AddChildren(values.Select(x => new LeafNode<T>() { Value = x }));

    public virtual void CreateNewComposite(T value, NodeMeta meta = null) =>
      AddChildren(new CompositeNode<T>() { Value = value, Meta = meta });

    public virtual void CreateNewComposites(IEnumerable<T> values) =>
      AddChildren(values.Select(x => new CompositeNode<T>() { Value = x }));

    #endregion

    #region Get Descendents Methods

    public virtual IEnumerable<Node<T>> GetDescendents()
    {
      return Descendants(i => i.Children);
    }

    public virtual IEnumerable<Node<T>> Descendants(Func<Node<T>, IEnumerable<Node<T>>> descendBy)
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

    public virtual IEnumerable<Node<T>> FindNodes(Func<Node<T>, bool> finder)
    {
      return Descendants(i => i.Children).Where(finder);
    }

    public virtual IEnumerable<Node<T>> FindLeafNodes(Func<Node<T>, bool> finder)
    {
      return Descendants(i => i.Children).Where(x => x.IsLeaf).Where(finder);
    }

    public virtual IEnumerable<Node<T>> FindCompositeNodes(Func<Node<T>, bool> finder)
    {
      return Descendants(i => i.Children).Where(x => !x.IsLeaf).Where(finder);
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



}
