using System;
using System.Collections.Generic;
using System.Text;

namespace compose.Models.Generic
{
  public class LeafNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    public LeafNode():base() { }

    public LeafNode(T value) : base(value) { }

    #endregion

    public override bool IsLeaf { get; protected set; } = true;
  }
}
