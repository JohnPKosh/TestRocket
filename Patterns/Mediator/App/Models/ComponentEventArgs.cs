using System;

namespace mediate.Models
{
  /// <summary>
  /// A public enumeration used to track if a component is being Attached or Detatched.
  /// </summary>
  /// <remarks>
  /// NOTE - The below enumeration could be expanded along with adding additional
  /// concrete implemenatations of ComponentEventArgs to handle sending messages etc.
  /// </remarks>
  public enum ComponentActionType
  {
    Attached,
    Detached
  }

  /// <summary>
  /// The abstract ComponentEventArgs class responsible for passing actions and component values.
  /// </summary>
  public abstract class ComponentEventArgs : EventArgs
  {
    /// <summary> The default constructor of this abstract class </summary>
    public ComponentEventArgs(ComponentActionType action) => Action = action;

    /// <summary> The public string component name (string used for simplicity) </summary>
    public string ComponentName { get; set; }

    /// <summary> The public ComponentActionType property defined by inheritors </summary>
    public ComponentActionType Action { get; protected set; }
  }

  /// <summary>
  /// The Component Attach Event Args to use when we attach components to service modules.
  /// </summary>
  public class ComponentAttachEventArgs : ComponentEventArgs
  {
    /// <summary> The default constructor that accepts only a name. ComponentActionType.Attached is forwarded to the base </summary>
    public ComponentAttachEventArgs(string name) : base(ComponentActionType.Attached) => ComponentName = name;
  }

  /// <summary>
  /// The Component Detach Event Args to use when we detach components from service modules.
  /// </summary>
  public class ComponentDetachEventArgs : ComponentEventArgs
  {
    /// <summary> The default constructor that accepts only a name. ComponentActionType.Detached is forwarded to the base </summary>
    public ComponentDetachEventArgs(string name) : base(ComponentActionType.Detached) => ComponentName = name;
  }
}
