using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MetaEditor.Controls
{
  public partial class TextBox
  {
    private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
    {
      PerformValidation();
    }

    private void txtValue_Loaded(object sender, RoutedEventArgs e)
    {
      PerformValidation();
    }
  }
}
