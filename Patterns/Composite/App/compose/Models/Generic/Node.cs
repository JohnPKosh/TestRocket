using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace compose.Models.Generic
{
  public abstract class Node<T> //: IEnumerable<Node<T>>
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

    [JsonIgnore]
    public Node<T> Parent { get; set; } = null;

    [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
    public List<Node<T>> Children { get; set; } = new List<Node<T>>();

    [JsonIgnore]
    public IEnumerable<Node<T>> Siblings => Parent?.Children.Where(x=> !x.Equals(this));

    #endregion

    #region Basic Public Methods

    public virtual void ReParent(CompositeNode<T> newParent)
    {
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      Parent.RemoveChildren(new[] { this });
      Parent = newParent;
      newParent.AddChildren(new[]{ this});
    }

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

    #region Get Descendents Methods

    [JsonIgnore]
    public virtual IEnumerable<Node<T>> DescendantsAndSelf => GetDescendants(true);

    [JsonIgnore]
    public virtual IEnumerable<Node<T>> Descendants => GetDescendants(false);

    protected virtual IEnumerable<Node<T>> GetDescendants(bool includeSelf = true)
    {
      if (includeSelf) yield return this;
      foreach (Node<T> child in Children)
      {
        foreach (var c in child.GetDescendants())
        {
          yield return c;
        }
      }
    }

    public virtual IEnumerable<Node<T>> FindNodes(Func<Node<T>, bool> finder, bool includeSelf = true)
    {
      return GetDescendants(includeSelf).Where(finder);
    }

    public virtual IEnumerable<Node<T>> FindLeafNodes(Func<Node<T>, bool> finder, bool includeSelf = true)
    {
      return GetDescendants(includeSelf).Where(x => x.IsLeaf).Where(finder);
    }

    public virtual IEnumerable<Node<T>> FindCompositeNodes(Func<Node<T>, bool> finder, bool includeSelf = true)
    {
      return GetDescendants(includeSelf).Where(x => !x.IsLeaf).Where(finder);
    }

    #endregion

  }
}
