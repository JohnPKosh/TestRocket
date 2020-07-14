using System;
using System.Collections.Generic;
using System.Linq;

namespace compose.Models.Generic
{
  public class CompositeNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    public CompositeNode() : base() { }

    public CompositeNode(T value, NodeMeta meta = null) : base(value, meta) { }

    #endregion

    public override bool IsLeaf { get; protected set; } = false;


    #region Basic Public Methods

    public virtual void AddChild(Node<T> child)
    {
      if (child == null) throw new ArgumentNullException(nameof(child));
      if (ReferenceEquals(this, child)) throw new ArgumentException(ADD_SELF_MSG, nameof(child));

      Children.Add(child);
      child.Parent = this;
    }

    public virtual void AddChildren(IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      if (ReferenceEquals(this, childNodes)) throw new ArgumentException(ADD_SELF_MSG, nameof(childNodes));

      foreach (var child in childNodes)
      {
        Children.Add(child);
        child.Parent = this;
      }
    }

    public virtual bool RemoveChild(Node<T> child)
    {
      if (child == null) throw new ArgumentNullException(nameof(child));
      if (ReferenceEquals(this, child)) throw new ArgumentException(REMOVE_SELF_MSG, nameof(child));

      if (Children.Remove(child))
      {
        child.Parent = null;
        return true;
      }
      return false;
    }

    public virtual void RemoveChildren(IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      foreach (var child in childNodes)
      {
        RemoveChild(child);
      }
    }

    public virtual void ReParentChildren(CompositeNode<T> newParent, IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      RemoveChildren(childNodes);
      foreach (var child in childNodes)
      {
        if (RemoveChild(child)) child.ReParent(newParent);
      }
    }

    public virtual void ReParentChildren(CompositeNode<T> newParent)
    {
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      ReParentChildren(newParent, Children);
    }

    public virtual void ReParentChildrenWhere(CompositeNode<T> newParent, Func<Node<T>, bool> predicate)
    {
      if (predicate == null) throw new ArgumentNullException(nameof(predicate));
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      ReParentChildren(newParent, Children.Where(predicate));
    }

    #endregion

    #region Create New Child Methods

    public virtual void CreateNewLeaf(T value, NodeMeta meta = null) =>
      AddChild(new LeafNode<T>() { Value = value, Meta = meta });

    public virtual void CreateNewLeaves(IEnumerable<T> values) =>
      AddChildren(values.Select(x => new LeafNode<T>() { Value = x }));

    public virtual void CreateNewComposite(T value, NodeMeta meta = null) =>
      AddChild(new CompositeNode<T>() { Value = value, Meta = meta });

    public virtual void CreateNewComposites(IEnumerable<T> values) =>
      AddChildren(values.Select(x => new CompositeNode<T>() { Value = x }));

    #endregion


  }
}
