using System;
using System.Collections.Generic;
using System.Text;
using compose.Models.Generic;

namespace compose.Models.Concrete
{
  public class RobotInstance :LeafNode<Robot>
  {
    public RobotInstance(Robot value, string robotName, string robotGroup)
    {
      Value = value;
      Meta.Name = Guid.NewGuid().ToString();
      Meta.GroupName = robotGroup;
      Meta.DisplayName = robotName;
    }
  }
}
