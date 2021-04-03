using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CompositeLibrary.Infrastructure.Models.Generic
{
  /// <summary>
  /// The public abstract base generic class for composite object graphs
  /// </summary>
  public abstract class Node<T>
  {
    protected const string ADD_SELF_MSG = "You cannot add a node to itself!";
    protected const string REMOVE_SELF_MSG = "You cannot remove a node from itself!";
    protected const string REPARENT_SELF_MSG = "You cannot reparent a node to itself!";

    #region Constructors and Class Initialization

    /// <summary>
    /// The default parameterless constructor
    /// </summary>
    public Node() { }

    /// <summary>
    /// The base constructor accepting a T value and optional NodeMeta
    /// </summary>
    public Node(T value, NodeMeta meta = null)
    {
      Value = value;
      if(meta != null) Meta = meta;
    }

    #endregion

    #region Fields and Props

    /// <summary>
    /// The T Value property of the class
    /// </summary>
    public T Value { get; set; } = default;

    /// <summary>
    /// The public abstract property indicating if this is a leaf
    /// </summary>
    public abstract bool IsLeaf { get; protected set; }

    /// <summary>
    /// The public NodeMeta property to contain metadata
    /// </summary>
    public NodeMeta Meta { get; set; } = new NodeMeta();

    /// <summary>
    /// The public Parent property of the node
    /// </summary>
    [JsonIgnore]
    public CompositeNode<T> Parent { get; set; } = null;

    /// <summary>
    /// The public Children property collection
    /// </summary>
    [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
    public List<Node<T>> Children { get; set; } = new List<Node<T>>();

    /// <summary>
    /// The public readonly Siblings property (calculated)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<Node<T>> Siblings => Parent?.Children.Where(x=> !x.Equals(this));

    /// <summary>
    /// The public readonly Descendants and Self property (calculated)
    /// </summary>
    [JsonIgnore]
    public virtual IEnumerable<Node<T>> DescendantsAndSelf => GetDescendants(true);

    /// <summary>
    /// The public readonly Descendants property (calculated)
    /// </summary>
    [JsonIgnore]
    public virtual IEnumerable<Node<T>> Descendants => GetDescendants(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// The public virtual method to update the current object's parent
    /// </summary>
    public virtual void ReParent(CompositeNode<T> newParent)
    {
      if (newParent == null) throw new ArgumentNullException(nameof(newParent));
      if (ReferenceEquals(this, newParent)) throw new ArgumentException(REPARENT_SELF_MSG, nameof(newParent));

      Parent.RemoveChildren(new[] { this });
      Parent = newParent;
      newParent.AddChildren(new[] { this });
    }

    /// <summary>
    /// The protected method to recursively walk the descendants and optional self
    /// </summary>
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

    /// <summary>
    /// The public virtual method to find all nodes matching a specific predicate
    /// </summary>
    public virtual IEnumerable<Node<T>> FindNodes(Func<Node<T>, bool> predicate, bool includeSelf = true)
    {
      return GetDescendants(includeSelf).Where(predicate);
    }

    /// <summary>
    /// The public virtual method to find leaf nodes matching a specific predicate
    /// </summary>
    public virtual IEnumerable<Node<T>> FindLeafNodes(Func<Node<T>, bool> predicate, bool includeSelf = true)
    {
      return GetDescendants(includeSelf).Where(x => x.IsLeaf).Where(predicate);
    }

    // <summary>
    /// The public virtual method to find composite nodes matching a specific predicate
    /// </summary>
    public virtual IEnumerable<Node<T>> FindCompositeNodes(Func<Node<T>, bool> predicate, bool includeSelf = true)
    {
      return GetDescendants(includeSelf).Where(x => !x.IsLeaf).Where(predicate);
    }

    #endregion

  }
}
