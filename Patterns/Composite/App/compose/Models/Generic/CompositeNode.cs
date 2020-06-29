using System;
using System.Collections.Generic;
using System.Text;

namespace compose.Models.Generic
{
  public class CompositeNode<T> : Node<T>
  {
    #region Constructors and Class Initialization

    public CompositeNode() : base() { }

    public CompositeNode(T value) : base(value) { }

    #endregion

    public override bool IsLeaf { get; protected set; } = false;
  }
}
