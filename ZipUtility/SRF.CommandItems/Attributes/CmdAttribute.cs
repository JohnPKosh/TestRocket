using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SRF.CommandItems.Attributes
{
  [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
  public sealed class CmdAttribute : Attribute
  {
    public CmdAttribute(string name, string aliases = null, string description = null)
    {
      Name = name;
      Aliases = aliases;
      Description = description;
    }

    #region Fields and Properties

    public string Name { get; private set; }

    public string Aliases { get; private set; }

    public string Description { get; private set; }

    #endregion

    public static Dictionary<string, Cmd> GetCommandDictionary(Type t)
    {
      var rv = new Dictionary<string, Cmd>();
      foreach (var c in GetCommands(t))
      {
        rv.TryAdd(c.Attribute.Aliases, c);
      }
      return rv;
    }

    public static IEnumerable<Cmd> GetCommands(Type t)
    {
      return t.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static)
        .SelectMany(x => GetCommands(x));
    }

    public static IEnumerable<Cmd> GetCommands(MethodInfo method)
    {
      return method.GetCustomAttributes<CmdAttribute>()
        .Select(x => new Cmd() { Attribute = x, Method = method });
    }
  }

  public class Cmd
  {
    public CmdAttribute Attribute { get; set; }

    public MethodInfo Method { get; set; }

    public IEnumerable<string> GetAliases()
    {
      return Attribute.Aliases?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
    }

    public Command GetCommand()
    {
      var rv = new Command(Attribute.Name, Attribute.Description);
      var cmdAliases = GetAliases();
      if (cmdAliases != null)
      {
        foreach (var alias in GetAliases())
        {
          rv.AddAlias(alias);
        }
      }

      var oattr = OptionAttribute.GetOptionParameters(Method);
      foreach (var o in oattr)
      {
        var option = new Option<object>(o.Attribute.Name, o.Attribute.Description);   // TODO: determine if we need to supply type specific mappings
        var aliases = o.Attribute.Aliases?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
        if (aliases != null)
        {
          foreach (var a in aliases)
          {
            option.AddAlias(a);
          }
        }
        if (o.Attribute.Required) option.IsRequired = true;
        rv.AddOption(option);
      }
      rv.Handler = CommandHandler.Create(CreateDelegate(Method));
      return rv;
    }

    static Delegate CreateDelegate(MethodInfo method)
    {
      if (method == null)
      {
        throw new ArgumentNullException("method");
      }

      if (!method.IsStatic)
      {
        throw new ArgumentException("The provided method must be static.", "method");
      }

      if (method.IsGenericMethod)
      {
        throw new ArgumentException("The provided method must not be generic.", "method");
      }

      return method.CreateDelegate(Expression.GetDelegateType(
          (from parameter in method.GetParameters() select parameter.ParameterType)
          .Concat(new[] { method.ReturnType })
          .ToArray()));
    }
  }

  [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
  public sealed class OptionAttribute : Attribute
  {
    public OptionAttribute(string name, string aliases = null, string description = null, bool required = false)
    {
      Name = name;
      Aliases = aliases;
      Description = description;
      Required = required;
    }

    #region Fields and Properties

    public string Name { get; private set; }

    public string Aliases { get; private set; }

    public string Description { get; private set; }

    public bool Required { get; private set; }

    #endregion

    public static IEnumerable<CmdOption> GetOptionParameters(MethodInfo method)
    {
      var rv = new List<CmdOption>();
      foreach (var p in method.GetParameters())
      {
        var opt = p.GetCustomAttributes<OptionAttribute>()?
        .Select(x => new CmdOption() { Attribute = x }).FirstOrDefault();
        if (opt != null) rv.Add(opt);
      }
      return rv;
    }
  }

  public class CmdOption
  {
    public OptionAttribute Attribute { get; set; }

  }

}
