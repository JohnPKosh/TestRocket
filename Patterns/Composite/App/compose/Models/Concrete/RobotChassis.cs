using System;

namespace compose.Models.Concrete
{
  /// <summary>
  /// The public robot chassis model class
  /// </summary>
  public class RobotChassis
  {
    /// <summary>
    /// The number of arms on the robot
    /// </summary>
    public int? ArmCount { get; set; }

    /// <summary>
    /// The public serial number of the robot part
    /// </summary>
    public long? SerialNumber { get; set; }
  }
}
