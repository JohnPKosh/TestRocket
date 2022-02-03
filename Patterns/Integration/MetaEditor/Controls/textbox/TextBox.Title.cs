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

    public string? Title { get; set; }

    public FontWeight TitleFontWeight { get; set; }

    public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register("Required", typeof(bool), typeof(TextBox), new FrameworkPropertyMetadata(false));

    public bool IsRequired
    {
      get { return (bool)GetValue(IsRequiredProperty); }
      set { SetValue(IsRequiredProperty, value); }
    }

    public Visibility IsRequiredVisibility
    {
      get
      {
        try
        {
          if (IsRequired) return Visibility.Visible;
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
