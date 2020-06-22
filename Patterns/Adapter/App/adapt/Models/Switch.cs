using adapt.Models.Interfaces;

namespace adapt.Models
{
  public class Switch : ISwitch
  {
    public void FlipSwitch()
    {
      FlippedCount++;
    }

    public int FlippedCount { get; set; } = 0;
  }
}
