using System.Collections.Generic;

namespace CompositeLibrary.Infrastructure.Models.Generic
{
  /// <summary>
  /// Public model class to store node meta data
  /// </summary>
  public class NodeMeta
  {
    /// <summary>
    /// The public display name string property
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// The public name string property
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The public node group name string property
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// Additional <![CDATA[Dictionary<string, string>]]> property bag
    /// </summary>
    public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
  }
}
