using System.Collections.Generic;

namespace compose.Models.Generic
{
  public class NodeMeta
  {
    public string DisplayName { get; set; }

    public string Name { get; set; }

    public string GroupName { get; set; }

    public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
  }
}
