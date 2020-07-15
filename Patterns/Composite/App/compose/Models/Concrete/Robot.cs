using System;
using compose.Models.Generic;

namespace compose.Models.Concrete
{
  /// <summary>
  /// The concrete robot leaf node class
  /// </summary>
  public class Robot :LeafNode<RobotChassis>
  {
    /// <summary>
    /// The public default constructor accepting a value, name and group
    /// </summary>
    public Robot(RobotChassis value, string robotName, string robotGroup)
    {
      Value = value;
      Meta.Name = Guid.NewGuid().ToString();
      Meta.GroupName = robotGroup;
      Meta.DisplayName = robotName;
    }
  }
}
