using System;
using compose.Models.Generic;

namespace compose.Models.Concrete
{
  /// <summary>
  /// A public concrete class composite RobotChassis container node
  /// </summary>
  public class RobotContainer : CompositeNode<RobotChassis>
  {
    private const string GROUP_NAME = "Robot Container";

    /// <summary>
    /// The public default constructor accepting a container name
    /// </summary>
    public RobotContainer(string containerName)
    {
      Meta.Name = Guid.NewGuid().ToString();
      Meta.GroupName = GROUP_NAME;
      Meta.DisplayName = containerName;
    }

    /// <summary>
    /// Additional public method overload to simplify node meta creation
    /// </summary>
    public void AddChildComposite(RobotChassis value, string containerName)
    {
      var meta = new NodeMeta
      {
        Name = Guid.NewGuid().ToString(),
        GroupName = GROUP_NAME,
        DisplayName = containerName
      };
      base.AddChildComposite(value, meta);
    }

    /// <summary>
    /// The overriden public method to add a child composite (custom logic attached)
    /// </summary>
    public override void AddChildComposite(RobotChassis value, NodeMeta meta)
    {
      if (meta != null && string.IsNullOrWhiteSpace(meta.GroupName)) meta.GroupName = GROUP_NAME;
      base.AddChildComposite(value, meta);
    }
  }
}
