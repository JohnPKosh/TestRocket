namespace compose.Models.Generic
{
  public class LeafNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    public LeafNode():base() { }

    public LeafNode(T value, NodeMeta meta = null) : base(value, meta) { }

    #endregion

    public override bool IsLeaf { get; protected set; } = true;

    //public override bool LockRoot { get; protected set; } = false;
  }
}
