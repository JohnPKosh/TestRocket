using System;
using System.Collections.Generic;
using System.Linq;

namespace mediate.Models
{
  public class ModuleController
  {
    #region Fields and Properties

    private const string EVENT_MSG_TEMPLATE = "A {0} has been [{1}] to the {2} [module].";
    private const string MODULE_NOT_FOUND = "The module specified was not found!";
    private const string NOT_SERVICE_MODULE = "Sender is not a ServiceModel!";
    private const StringComparison STR_COMPARISON = StringComparison.InvariantCultureIgnoreCase;

    private List<ServiceModule> m_Modules { get; set; } = new List<ServiceModule>();

    #endregion

    #region Public Methods

    public void AttachModule(string name)
    {
      var module = new ServiceModule(name);
      module.Controller = this;
      module.AttachHandler += m_AttachHandler;
      module.DetachHandler += m_DetachHandler;
      m_Modules.Add(module);
    }

    public void DetachModule(string name)
    {
      var module = m_Modules.FirstOrDefault(x => x.ModuleName.Equals(name, STR_COMPARISON));
      if (module != null)
      {
        module.Controller = null;
        module.AttachHandler -= m_AttachHandler;
        module.DetachHandler -= m_DetachHandler;
        m_Modules.Remove(module);
      }
    }

    public void AttachComponentToModule(string moduleName, string component)
    {
      var mod = m_Modules.FirstOrDefault(x => x.ModuleName.Equals(moduleName, STR_COMPARISON));
      if (mod != null) mod.AttachComponent(component);
      else Console.WriteLine(MODULE_NOT_FOUND);
    }

    public void DetachComponentFromModule(string moduleName, string component)
    {
      var mod = m_Modules.FirstOrDefault(x => x.ModuleName.Equals(moduleName, STR_COMPARISON));
      if (mod != null) mod.DetachComponent(component);
      else Console.WriteLine(MODULE_NOT_FOUND);
    }

    #endregion

    #region Private Utility Methods

    #region *** Event Handler Methods

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
