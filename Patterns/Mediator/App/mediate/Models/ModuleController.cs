using System;
using System.Collections.Generic;
using System.Linq;

namespace mediate.Models
{
  /// <summary>
  /// The module controller class acts as a control unit (mediator) for attaching
  /// and providing power for service modules that can in turn connect components.
  /// The one rule our mediator enforces is that there should be no duplicate
  /// attached components accross all of the attached modules. The Service Modules
  /// however have no idea about the other service modules or how this is done since
  /// they rely on the mediator to enforce that logic.
  /// </summary>
  /// <remarks> We could add additional message logic like in the chat room if needed </remarks>
  public class ModuleController
  {
    #region Fields and Properties

    private const string EVENT_MSG_TEMPLATE = "A {0} [component] has been [{1}] to the {2} [module].";
    private const string MODULE_NOT_FOUND = "The module specified was not found!";
    private const string NOT_SERVICE_MODULE = "Sender is not a ServiceModel!";
    private const StringComparison STR_COMPARISON = StringComparison.InvariantCultureIgnoreCase;

    private List<ServiceModule> m_Modules { get; set; } = new List<ServiceModule>();

    #endregion

    #region Public Methods

    /// <summary> Overloaded method to allow the controller to attach a module by name </summary>
    public void AttachModule(string name) => AttachModule(new ServiceModule(name));

    /// <summary> Method to allow the controller to attach a module </summary>
    public void AttachModule(ServiceModule module)
    {
      module.Controller = this;
      module.AttachedEventHandler += m_AttachHandler;
      module.DetachedEventHandler += m_DetachHandler;
      m_Modules.Add(module);
    }

    /// <summary> Method to allow the controller to detach a module </summary>
    public void DetachModule(string name)
    {
      var module = m_Modules.FirstOrDefault(x => x.ModuleName.Equals(name, STR_COMPARISON));
      if (module != null)
      {
        module.Controller = null;
        module.AttachedEventHandler -= m_AttachHandler;
        module.DetachedEventHandler -= m_DetachHandler;
        m_Modules.Remove(module);
      }
    }

    /// <summary> Method to allow the controller to attach a component to a module </summary>
    public void AttachComponentToModule(string moduleName, string component)
    {
      var mod = m_Modules.FirstOrDefault(x => x.ModuleName.Equals(moduleName, STR_COMPARISON));
      if (mod != null) mod.AttachComponent(component);
      else Console.WriteLine(MODULE_NOT_FOUND);
    }

    /// <summary> Method to allow the controller to detach a component to a module </summary>
    public void DetachComponentFromModule(string moduleName, string component)
    {
      var mod = m_Modules.FirstOrDefault(x => x.ModuleName.Equals(moduleName, STR_COMPARISON));
      if (mod != null) mod.DetachComponent(component);
      else Console.WriteLine(MODULE_NOT_FOUND);
    }

    /// <summary> Method that checks if a component already exists somewhere else in any module </summary>
    public bool ComponentExists(string name)
      => m_Modules.SelectMany(m => m.AttachedComponents.Where(c => name == c)).Any();

    #endregion

    #region Private Utility Methods

    #region *** Event Handler Methods

    // THIS is where we provide our handler logic that will be registered on each ServiceModule.
    // Because we provided some clever inheritance structure we can now define the below methods
    // to accept an abstract ComponentEventArgs and both of the below will in tunr call the same
    // private SendChangeMsg method, but the results will vary appropriatly due to inheritance.

    private void m_AttachHandler(object sender, ComponentEventArgs e) => SendChangeMsg(sender, e);

    private void m_DetachHandler(object sender, ComponentEventArgs e) => SendChangeMsg(sender, e);

    #endregion

    private static void SendChangeMsg(object sender, ComponentEventArgs e)
    {
      if (!(sender is ServiceModule)) throw new ArgumentException(NOT_SERVICE_MODULE);
      var module = sender as ServiceModule;
      Console.WriteLine(EVENT_MSG_TEMPLATE, e.ComponentName, e.Action, module.ModuleName);
    }

    #endregion
  }
}