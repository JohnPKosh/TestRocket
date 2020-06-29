using System;
using System.Collections.Generic;
using System.Text;

namespace compose.Models.Concrete
{
  public class RobotChassis
  {
    public int? ArmCount { get; set; }

    public string GetGuid()
    {
      return Guid.NewGuid().ToString();
    }
  }
}
