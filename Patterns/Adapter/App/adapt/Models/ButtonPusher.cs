using adapt.Models.Interfaces;

namespace adapt.Models
{
  /// <summary>
  /// The primary button pusher class.
  /// </summary>
  public class ButtonPusher : IPushButton
  {
    /// <summary> The primary method of a button is to push it, push it real good. </summary>
    public void Push()
    {
      PushedCount++;
    }

    /// <summary> Property to track how many time you have pushed my button. </summary>
    public int PushedCount { get; set; } = 0;
  }
}
