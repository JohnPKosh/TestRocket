using System;
using System.Collections.Generic;
using System.Linq;

namespace mediate.Models
{
  /// <summary>
  /// The service module class that will interact through the ModuleController (mediator).
  /// </summary>
  public class ServiceModule
  {
    #region Class Initialization and Constructors

    /// <summary> The default constructor for a ServiceModule that accepts a name </summary>
    public ServiceModule(string name) => ModuleName = name;

    #endregion

    #region Fields and Properties

    /// <summary> Public event handler property used to handle the attach component events </summary>
    public event EventHandler<ComponentEventArgs> AttachedEventHandler;

    /// <summary> Public event handler property used to handle the detach component events </summary>
    public event EventHandler<ComponentEventArgs> DetachedEventHandler;

    /// <summary> Public name of the module </summary>
    public string ModuleName { get; private set; }

    /// <summary> Public list of attached components on the service module </summary>
    public List<string> AttachedComponents { get; set; } = new List<string>();

    /// <summary> Public property of the ModuleController that this module is attached to </summary>
    /// <remarks> In order to coordinate with other modules this needs a mediator to attach to</remarks>
    public ModuleController Controller { get; set; }

    #endregion


    #region Public Methods

    #region *** Attach Specific Methods

    /// <summary> Public method to attach this service module to a ModuleController </summary>
    public void AttachToController(ModuleController controller)
    {
      controller.AttachModule(this);
    }

    /// <summary> Public method used to attach a new component by name </summary>
    public void AttachComponent(string componentName)
    {
      if(Controller != null)
      {
        if (!Controller.ComponentExists(componentName))
        {
          AttachedComponents.Add(componentName);
          // Once we have attached the component above we will call the OnAttachComponent
          // method to invoke the AttachedEventHandler that the ModuleController responds
          // to using the (m_AttachHandler) method
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

    /// <summary> Protected virtual method that invokes the AttachedEventHandler </summary>
    protected virtual void OnAttachComponent(ComponentAttachEventArgs args)
    {
      AttachedEventHandler?.Invoke(this, args);
    }

    #endregion

    #region *** Detach Specific Methods

    /// <summary> Public method used to detach a new component by name </summary>
    public void DetachComponent(string componentName)
    {
      var c = AttachedComponents.FirstOrDefault(x => x == componentName);
      if (c != null)
      {
        AttachedComponents.Remove(componentName);
        // Once we have detached the component above we will call the OnDetachComponent
        // method to invoke the DetachedEventHandler that the ModuleController responds
        // to using the (m_DetachHandler) method
        var component = new ComponentDetachEventArgs(componentName);
        OnDetachComponent(component);
      }
      else
      {
        Console.WriteLine("Component {0} is not attached to this module.");
      }
    }

    /// <summary> Protected virtual method that invokes the DetachedEventHandler </summary>
    protected virtual void OnDetachComponent(ComponentDetachEventArgs args)
    {
      DetachedEventHandler?.Invoke(this, args);
    }

    #endregion

    #endregion
  }
}
