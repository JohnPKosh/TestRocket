namespace compose.Models.Generic
{
  public class CompositeNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    public CompositeNode() : base() { }

    public CompositeNode(T value, NodeMeta meta = null) : base(value, meta) { }

    #endregion

    public override bool IsLeaf { get; protected set; } = false;

    public override bool LockRoot { get; protected set; } = false;
  }
}
