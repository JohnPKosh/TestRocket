using System;
using System.Collections.Generic;
using System.Text;
using compose.Models.Generic;

namespace compose.Models.Concrete
{
  public class RobotContainer : CompositeNode<Robot>
  {
    private const string GROUP_NAME = "Robot Container";

    public RobotContainer(string containerName)
    {
      Meta.Name = Guid.NewGuid().ToString();
      Meta.GroupName = GROUP_NAME;
      Meta.DisplayName = containerName;
    }

    public void CreateNewComposite(Robot value, string containerName)
    {
      var meta = new NodeMeta
      {
        Name = Guid.NewGuid().ToString(),
        GroupName = GROUP_NAME,
        DisplayName = containerName
      };
      base.CreateNewComposite(value, meta);
    }

    public override void CreateNewComposite(Robot value, NodeMeta meta)
    {
      if (string.IsNullOrWhiteSpace(meta.GroupName)) meta.GroupName = GROUP_NAME;
      base.CreateNewComposite(value, meta);
    }
  }
}
