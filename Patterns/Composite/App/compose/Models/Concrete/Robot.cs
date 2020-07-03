using System;
using compose.Models.Generic;

namespace compose.Models.Concrete
{
  public class Robot :LeafNode<RobotChassis>
  {
    public Robot(RobotChassis value, string robotName, string robotGroup)
    {
      Value = value;
      Meta.Name = Guid.NewGuid().ToString();
      Meta.GroupName = robotGroup;
      Meta.DisplayName = robotName;
    }
  }
}
