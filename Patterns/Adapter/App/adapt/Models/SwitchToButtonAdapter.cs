using adapt.Models.Interfaces;

namespace adapt.Models
{
  /// <summary>
  /// This is an adapter pattern class that implements IPushButton that
  /// will allow you to push a button instead of needing to flip a switch.
  /// </summary>
  public class SwitchToButtonAdapter : IPushButton
  {
    /// <summary>
    /// The default constructor accepting an IFlipSwitch implementation
    /// to adapt to.
    /// </summary>
    public SwitchToButtonAdapter(IFlipSwitch @switch)
    {
      m_Switch = @switch;
    }

    private IFlipSwitch m_Switch { get; set; }

    /// <summary>
    /// Method to push my button which causes me to flip my switch!
    /// </summary>
    /// <remarks>Not possible without an adapter.</remarks>
    public void Push()
    {
      m_Switch.FlipSwitch();
    }

    /// <summary>
    /// The number of times you pushed my switch!
    /// </summary>
    /// <remarks>Not possible without an adapter.</remarks>
    public int PushedCount
    {
      get { return m_Switch.FlippedCount; }
      set { m_Switch.FlippedCount = value; }
    }
  }
}
