using adapt.Models.Interfaces;

namespace adapt.Models
{
  /// <summary>
  /// The primary switch flipper class.
  /// </summary>
  public class SwitchFlipper : IFlipSwitch
  {
    /// <summary>
    /// Flip the switch method.
    /// </summary>
    public void FlipSwitch()
    {
      FlippedCount++;
    }

    /// <summary>
    /// Property to track the number of times you flipped by switch.
    /// </summary>
    public int FlippedCount { get; set; } = 0;
  }
}
