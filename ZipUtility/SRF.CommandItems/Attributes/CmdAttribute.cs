using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SRF.CommandItems.Attributes
{
  /// <summary>
  /// The Cmd Attribute used for bootstrapping arguments to command line methods.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
  public sealed class CmdAttribute : Attribute
  {
    /// <summary>
    /// The attribute constructor accepting a name plus optional aliases and description
    /// </summary>
    public CmdAttribute(string name, string aliases = null, string description = null)
    {
      Name = name;
      Aliases = aliases;
      Description = description;
    }

    #region Fields and Properties

    /// <summary>
    /// The command line "Command" name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Comma seperated string of additional "Command" aliases.
    /// </summary>
    public string Aliases { get; private set; }

    /// <summary>
    /// The description of the command displayed using the --help (-?, -h)
    /// </summary>
    public string Description { get; private set; }

    #endregion

    /// <summary>
    /// Gets all CmdAttribute decorated methods as Cmd objects for registration in Bootstrap.cs
    /// </summary>
    public static IEnumerable<Cmd> GetCommands(Type t)
    {
      return t.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static)
        .SelectMany(x => getCommandMethods(x));
    }

    private static IEnumerable<Cmd> getCommandMethods(MethodInfo method)
    {
      return method.GetCustomAttributes<CmdAttribute>()
        .Select(x => new Cmd() { Attribute = x, Method = method });
    }
  }

  /// <summary>
  /// Wraps and generates the System.CommandLine.Command objects for registration in Bootstrap.cs
  /// </summary>
  public class Cmd
  {
    /// <summary>
    /// The CmdAttribute property value decorated on the method
    /// </summary>
    public CmdAttribute Attribute { get; set; }

    /// <summary>
    /// The decorated method's MethodInfo property
    /// </summary>
    public MethodInfo Method { get; set; }

    /// <summary>
    /// Returns an <![CDATA[IEnumerable<string>]]> of Aliases on the CmdAttribute
    /// </summary>
    public IEnumerable<string> GetAliases()
    {
      return Attribute.Aliases?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
    }

    /// <summary>
    /// Public method used to compile a Command object from this Cmd item.
    /// </summary>
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
        var option = new Option<object>(o.Name, o.Description);
        var aliases = o.Aliases?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
        if (aliases != null)
        {
          foreach (var a in aliases)
          {
            option.AddAlias(a);
          }
        }
        if (o.Required) option.IsRequired = true;
        rv.AddOption(option);
      }
      rv.Handler = CommandHandler.Create(CreateDelegate(Method));
      return rv;
    }

    /// <summary>Private utility used to create a Delegate for a CommandHandler</summary>
    private static Delegate CreateDelegate(MethodInfo method)
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

  /// <summary>
  /// The OptionAttribute for method parameters that builds System.CommandLine Option items in Bootstrap.cs
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
  public sealed class OptionAttribute : Attribute
  {
    /// <summary>
    /// The attribute constructor that accepts a name and optional aliases, description, and required parameters.
    /// </summary>
    /// <remarks>
    /// *****IMPORTANT***** - The option attribute name and the parameter name must be identical including case!
    /// </remarks>
    /// <example>
    /// [Option("--ntimes", "-n,\\n", description: "Number of times to call the method", required: false)] int ntimes
    /// </example>
    public OptionAttribute(string name, string aliases = null, string description = null, bool required = true)
    {
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The OptionAttribute Name Must not be null or whitespace.", nameof(name));
      Name = name;
      Aliases = aliases;
      Description = description;
      Required = required;
    }

    #region Fields and Properties

    /// <summary>
    /// The Option Name property mapped to System.CommandLine args and the parameter name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Comma seperated string of additional "Option" aliases.
    /// </summary>
    public string Aliases { get; private set; }

    /// <summary>
    /// The description of the option displayed using the --help (-?, -h)
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Indicates if the command line arg is optional
    /// default is true (supply a default parameter value on the method if false)
    /// </summary>
    public bool Required { get; private set; }

    #endregion

    /// <summary>
    /// Returns an <![CDATA[IEnumerable<CmdOption>]]> used to generate Command Option args
    /// </summary>
    public static IEnumerable<OptionAttribute> GetOptionParameters(MethodInfo method)
    {
      var rv = new List<OptionAttribute>();
      foreach (var p in method.GetParameters())
      {
        var opt = p.GetCustomAttributes<OptionAttribute>()?.FirstOrDefault();
        if (opt != null) rv.Add(opt);
      }
      return rv;
    }
  }
}
