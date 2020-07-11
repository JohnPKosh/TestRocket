using System;

namespace mediate.Models
{
  public class ServiceModule
  {
    #region Class Initialization and Constructors

    public ServiceModule(string name) => ModuleName = name;

    #endregion

    #region Fields and Properties

    public event EventHandler<ComponentEventArgs> AttachHandler;

    public event EventHandler<ComponentEventArgs> DetachHandler;

    public string ModuleName { get; private set; }

    public ModuleController Controller { get; set; }

    #endregion


    #region Public Methods

    #region *** Attach Specific Methods

    public void AttachComponent(string componentName)
    {
      var component = new ComponentAttachEventArgs(componentName);
      OnAttachComponent(component);
    }

    protected virtual void OnAttachComponent(ComponentAttachEventArgs args)
    {
      AttachHandler?.Invoke(this, args);
    }

    #endregion

    #region *** Detach Specific Methods

    public void DetachComponent(string componentName)
    {
      var component = new ComponentDetachEventArgs(componentName);
      OnDetachComponent(component);
    }

    protected virtual void OnDetachComponent(ComponentDetachEventArgs args)
    {
      DetachHandler?.Invoke(this, args);
    }

    #endregion

    #endregion
  }
}
