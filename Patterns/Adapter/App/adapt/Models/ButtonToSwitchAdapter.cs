using adapt.Models.Interfaces;

namespace adapt.Models
{
  /// <summary>
  /// This is an adapter pattern class that implements IFlipSwitch that
  /// will allow you to flip a switch instead of needing to push a button.
  /// </summary>
  public class ButtonToSwitchAdapter : IFlipSwitch
  {
    /// <summary>
    /// The default constructor accepting an IPushButton implementation
    /// to adapt to.
    /// </summary>
    public ButtonToSwitchAdapter(IPushButton button)
    {
      m_Button = button;
    }

    private IPushButton m_Button { get; set; }

    /// <summary>
    /// Method to flip our switch that actually pushes a button.
    /// </summary>
    /// <remarks>Not possible without an adapter.</remarks>
    public void FlipSwitch()
    {
      m_Button.Push();
    }

    /// <summary>
    /// The number of times you flipped that button!
    /// </summary>
    /// <remarks>Not possible without an adapter.</remarks>
    public int FlippedCount
    {
      get { return m_Button.PushedCount; }
      set { m_Button.PushedCount = value; }
    }
  }
}
