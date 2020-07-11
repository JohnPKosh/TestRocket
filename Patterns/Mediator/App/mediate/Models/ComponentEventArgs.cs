using System;

namespace mediate.Models
{
  public enum ComponentActionType
  {
    Attached,
    Detached
  }

  public abstract class ComponentEventArgs : EventArgs
  {
    public ComponentEventArgs(ComponentActionType action) => Action = action;

    public string ComponentName { get; set; }

    public ComponentActionType Action { get; protected set; }
  }

  public class ComponentAttachEventArgs : ComponentEventArgs
  {
    public ComponentAttachEventArgs(string name) : base(ComponentActionType.Attached) => ComponentName = name;
  }

  public class ComponentDetachEventArgs : ComponentEventArgs
  {
    public ComponentDetachEventArgs(string name) : base(ComponentActionType.Detached) => ComponentName = name;
  }
}
