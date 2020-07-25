namespace compose.Models.Generic
{
  /// <summary>
  /// The public generic concrete leaf implementation of a node.
  /// </summary>
  public class LeafNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    /// <summary>
    /// The default parameterless constructor
    /// </summary>
    public LeafNode():base() { }

    /// <summary>
    /// Additional public constructor accepting a T value and node meta data
    /// </summary>
    public LeafNode(T value, NodeMeta meta = null) : base(value, meta) { }

    #endregion

    /// <summary>
    /// The overriden public property indicating that this IS a leaf node
    /// </summary>
    public override bool IsLeaf { get; protected set; } = true;
  }
}
