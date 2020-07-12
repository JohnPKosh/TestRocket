using System;
using System.Collections.Generic;
using System.Linq;

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

    public List<string> AttachedComponents { get; set; } = new List<string>();

    public ModuleController Controller { get; set; }

    #endregion


    #region Public Methods

    #region *** Attach Specific Methods

    public void AttachToController(ModuleController controller)
    {
      controller.AttachModule(this);
    }

    public void AttachComponent(string componentName)
    {
      if(Controller != null)
      {
        if (!Controller.ComponentExists(componentName))
        {
          AttachedComponents.Add(componentName);
          var component = new ComponentAttachEventArgs(componentName);
          OnAttachComponent(component);
        }
        else
        {
          Console.WriteLine("The {0} already exists in some other module. You can only have one per controller module.", componentName);
        }
      }
      else
      {
        Console.WriteLine("You cannot connect a module unless it is attached to a ModuleController.");
      }
    }

    protected virtual void OnAttachComponent(ComponentAttachEventArgs args)
    {
      AttachHandler?.Invoke(this, args);
    }

    #endregion

    #region *** Detach Specific Methods

    public void DetachComponent(string componentName)
    {
      var c = AttachedComponents.FirstOrDefault(x => x == componentName);
      if (c != null)
      {
        AttachedComponents.Remove(componentName);
        var component = new ComponentDetachEventArgs(componentName);
        OnDetachComponent(component);
      }
      else
      {
        Console.WriteLine("Component {0} is not attached to this module.");
      }
    }

    protected virtual void OnDetachComponent(ComponentDetachEventArgs args)
    {
      DetachHandler?.Invoke(this, args);
    }

    #endregion

    #endregion
  }
}
