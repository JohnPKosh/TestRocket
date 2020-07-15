using System;
using System.Collections.Generic;
using System.Linq;

namespace compose.Models.Generic
{
  /// <summary>
  /// The public generic concrete composite implementation of a node.
  /// </summary>
  public class CompositeNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    /// <summary> The default paramaterless constructor </summary>
    public CompositeNode() : base() { }

    /// <summary>
    /// The public constructor accepting a T value and meta data
    /// </summary>
    public CompositeNode(T value, NodeMeta meta = null) : base(value, meta) { }

    #endregion

    /// <summary>
    /// The overriden public property indicating that this IS NOT a leaf node
    /// </summary>
    public override bool IsLeaf { get; protected set; } = false;


    #region Basic Public Methods

    /// <summary>
    /// The public virtual method to add a child node of T
    /// </summary>
    public virtual void AddChild(Node<T> child)
    {
      if (child == null) throw new ArgumentNullException(nameof(child));
      if (ReferenceEquals(this, child)) throw new ArgumentException(ADD_SELF_MSG, nameof(child));

      Children.Add(child);
      child.Parent = this;
    }

    /// <summary>
    /// The public virtual method to add child nodes of T
    /// </summary>
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

    /// <summary>
    /// The public virtual method to remove the matching child node of T
    /// </summary>
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

    /// <summary>
    /// The public virtual method to remove the matching child nodes of T
    /// </summary>
    public virtual void RemoveChildren(IEnumerable<Node<T>> childNodes)
    {
      if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));
      foreach (var child in childNodes)
      {
        RemoveChild(child);
      }
    }

    /// <summary>
    /// The public virtual method to move matching child nodes of T to a new parent
    /// </summary>
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

    /// <summary>
    /// The public virtual method to move all current child nodes of T to a new parent
    /// </summary>
    public virtual void ReParentChildren(CompositeNode<T> newParent)
    {
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      ReParentChildren(newParent, Children);
    }

    /// <summary>
    /// The public virtual method to move all predicate matched current child nodes of T to a new parent
    /// </summary>
    public virtual void ReParentChildrenWhere(CompositeNode<T> newParent, Func<Node<T>, bool> predicate)
    {
      if (predicate == null) throw new ArgumentNullException(nameof(predicate));
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      ReParentChildren(newParent, Children.Where(predicate));
    }

    #endregion

    #region Create New Child Methods

    /// <summary>
    /// The public virtual factory method to create and add a new child leaf node
    /// </summary>
    public virtual void AddChildLeaf(T value, NodeMeta meta = null) =>
      AddChild(new LeafNode<T>() { Value = value, Meta = meta });

    /// <summary>
    /// The public virtual factory method to create and add a new child composite
    /// </summary>
    public virtual void AddChildComposite(T value, NodeMeta meta = null) =>
      AddChild(new CompositeNode<T>() { Value = value, Meta = meta });


    #endregion


  }
}
