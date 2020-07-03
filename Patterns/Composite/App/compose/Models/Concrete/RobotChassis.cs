using System;

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
