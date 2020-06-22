using System;
using System.Collections.Generic;
using System.Text;
using adapt.Models.Interfaces;

namespace adapt.Models
{
  public class SwitchToButtonAdapter : IButton
  {
    public SwitchToButtonAdapter(ISwitch @switch)
    {
      m_Switch = @switch;
    }

    private ISwitch m_Switch { get; set; }

    public void Push()
    {
      m_Switch.FlipSwitch();
    }

    public int PushedCount
    {
      get { return m_Switch.FlippedCount; }
      set { m_Switch.FlippedCount = value; }
    }
  }
}
