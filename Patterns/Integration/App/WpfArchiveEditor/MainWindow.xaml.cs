using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using ArchiveData.Models;
using ArchiveLogic.Generate;
using adata = Azos.Data;

namespace WpfArchiveEditor
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    public object[] values { get; set; }

    private void btn_Generate_Click(object sender, RoutedEventArgs e)
    {
      if (int.TryParse(txt_Quantity.Text, out int count))
      {
        trk_Index.Maximum = count - 1;
        trk_Index.Value = 0;

        // For fake logs we need to `Select` the instance property. With other FakeRow implementors it is not necessary.
        values = FakeBuilder.GenerateMany<FakeLogMessage>(1, 1, (ulong)count).Select(x => x.Instance).ToArray();
        txt_Item.Text = ((adata.TypedDoc)values[(int)trk_Index.Value]).ToJsonDataMap().ToString();
      }
    }

    private void trk_Index_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (values != null)
        txt_Item.Text = ((adata.TypedDoc)values[(int)e.NewValue]).ToJsonDataMap().ToString();
    }

    private void btn_Next_Click(object sender, RoutedEventArgs e)
    {
      if (values != null && trk_Index.Value < trk_Index.Maximum)
      {
        trk_Index.Value++;
        txt_Item.Text = ((adata.TypedDoc)values[(int)trk_Index.Value]).ToJsonDataMap().ToString();
      }        
    }

    private void btn_Previous_Click(object sender, RoutedEventArgs e)
    {
      if (values != null && trk_Index.Value > trk_Index.Minimum)
      {
        trk_Index.Value--;
        txt_Item.Text = ((adata.TypedDoc)values[(int)trk_Index.Value]).ToJsonDataMap().ToString();
      }
    }
  }
}
