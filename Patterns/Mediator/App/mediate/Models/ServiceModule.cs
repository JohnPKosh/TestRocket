using System;
using System.Collections.Generic;
using System.Linq;

namespace mediate.Models
{
  public class ModuleController
  {
    private const string EVENT_MSG_TEMPLATE = "A {0} has been [{1}] to the {2} [module].";
    private const string MODULE_NOT_FOUND = "The module specified was not found!";
    private const string NOT_SERVICE_MODULE = "Sender is not a ServiceModel!";
    private const StringComparison STR_COMPARISON = StringComparison.InvariantCultureIgnoreCase;

    private List<ServiceModule> m_Modules { get; set; } = new List<ServiceModule>();

    public void AttachModule(string name)
    {
      var module = new ServiceModule(name);
      module.AttachHandler += m_AttachHandler;
      module.DetachHandler += m_DetachHandler;
      m_Modules.Add(module);
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

    private void m_AttachHandler(object sender, ComponentEventArgs e) => SendChangeMsg(sender, e);

    private void m_DetachHandler(object sender, ComponentEventArgs e) => SendChangeMsg(sender, e);

    private static void SendChangeMsg(object sender, ComponentEventArgs e)
    {
      if (!(sender is ServiceModule)) throw new ArgumentException(NOT_SERVICE_MODULE);
      var module = sender as ServiceModule;
      Console.WriteLine(EVENT_MSG_TEMPLATE, e.ComponentName, e.Action, module.ModuleName);
    }
  }


  public class ServiceModule
  {
    public ServiceModule(string name) => ModuleName = name;

    public string ModuleName { get; private set; }

    public void AttachComponent(string componentName)
    {
      var component = new ComponentAttachEventArgs(componentName);
      OnAttachComponent(component);
    }

    public event EventHandler<ComponentEventArgs> AttachHandler;
    protected virtual void OnAttachComponent(ComponentAttachEventArgs args)
    {
        AttachHandler?.Invoke(this, args);
    }

    public void DetachComponent(string componentName)
    {
      var component = new ComponentDetachEventArgs(componentName);
      OnDetachComponent(component);
    }

    public event EventHandler<ComponentEventArgs> DetachHandler;
    protected virtual void OnDetachComponent(ComponentDetachEventArgs args)
    {
      DetachHandler?.Invoke(this, args);
    }

  }

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

  public class ComponentAttachEventArgs: ComponentEventArgs
  {
    public ComponentAttachEventArgs(string name) : base(ComponentActionType.Attached) => ComponentName = name;
  }

  public class ComponentDetachEventArgs : ComponentEventArgs
  {
    public ComponentDetachEventArgs(string name) : base(ComponentActionType.Detached) => ComponentName = name;
  }
}
