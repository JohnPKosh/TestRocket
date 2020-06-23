using adapt.Models.Interfaces;

namespace adapt.Models
{
  public class ButtonToSwitchAdapter : ISwitch
  {
    public ButtonToSwitchAdapter(IButton button)
    {
      m_Button = button;
    }

    private IButton m_Button { get; set; }

    public void FlipSwitch()
    {
      m_Button.Push();
    }

    public int FlippedCount
    {
      get { return m_Button.PushedCount; }
      set { m_Button.PushedCount = value; }
    }
  }
}
