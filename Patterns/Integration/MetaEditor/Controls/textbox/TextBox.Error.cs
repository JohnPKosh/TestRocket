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

    public static readonly DependencyProperty ErrorLabelProperty = DependencyProperty.Register("ErrorLabelText", typeof(string), typeof(TextBox), new FrameworkPropertyMetadata(string.Empty));

    public string? ErrorLabelText
    {
      get { return (string)GetValue(ErrorLabelProperty); }
      set { SetValue(ErrorLabelProperty, value); }
    }

  }
}
