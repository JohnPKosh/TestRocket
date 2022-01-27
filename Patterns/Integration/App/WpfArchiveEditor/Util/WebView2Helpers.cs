using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArchiveEditor.Util
{
  public static  class WebView2Helpers
  {

    public static void SetAppContextRelativeSource(this WebView2 ctrl, string relativeFileName)
    {
      var src = FileHelpers.MakeAppContextRelativeUri(relativeFileName);
      ctrl.Source = new Uri(src);
    }

    public static void SetProcessExeDirRelativeSource(this WebView2 ctrl, string relativeFileName)
    {
      var src = FileHelpers.MakeProcessExeRelativeUri(relativeFileName);
      ctrl.Source = new Uri(src);
    }
  }
}
