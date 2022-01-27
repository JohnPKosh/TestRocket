using MahApps.Metro.Controls;
using Microsoft.Web.WebView2.Wpf;
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
using System.Windows.Shapes;
using WpfArchiveEditor.Util;

namespace WpfArchiveEditor.Forms
{
  /// <summary>
  /// Interaction logic for TabDashboard.xaml
  /// </summary>
  public partial class TabDashboard : MetroWindow
  {
    public TabDashboard()
    {
      InitializeComponent();
    }

    private void webView_Loaded(object sender, RoutedEventArgs e)
    {
      var ctrl = (WebView2)sender;
      ctrl.SetAppContextRelativeSource("test.html");
    }
  }
}

// Getting started with webview2 https://docs.microsoft.com/en-us/microsoft-edge/webview2/gettingstarted/wpf
// Bootstrapper link for webview2  https://go.microsoft.com/fwlink/p/?LinkId=2124703