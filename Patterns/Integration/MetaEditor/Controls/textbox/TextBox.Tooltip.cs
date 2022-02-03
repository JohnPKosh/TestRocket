using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MetaEditor.Controls
{
  public partial class TextBox
  {

    public static readonly DependencyProperty TooltipProperty = DependencyProperty.Register("TextTooltip", typeof(string), typeof(TextBox), new FrameworkPropertyMetadata(string.Empty));

    public string? TooltipText
    {
      get { return (string)GetValue(TooltipProperty); }
      set { SetValue(TooltipProperty, value); }
    }

    public bool TooltipTextVisible
    {
      get
      {
        try
        {
          return !string.IsNullOrWhiteSpace(TooltipText);
        }
        catch (Exception)
        {
          return false;
        }
      }
    }

    public Visibility TooltipTextVisibility
    {
      get
      {
        try
        {
          if (TooltipTextVisible) return Visibility.Visible;
          return Visibility.Collapsed;
        }
        catch (Exception)
        {
          return Visibility.Collapsed;
        }
      }
    }
  }
}
