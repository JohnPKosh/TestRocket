using adapt.Models.Interfaces;

namespace adapt.Models
{
  public class Button : IButton
  {
    public void Push()
    {
      PushedCount++;
    }

    public int PushedCount { get; set; } = 0;
  }
}
