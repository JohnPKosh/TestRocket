using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinArchiveEditor
{
  public partial class SaveForm : Form
  {
    public SaveForm()
    {
      InitializeComponent();
      Init();
    }

    private void Init()
    {
      var ctl = cmb_CompressionType;
      ctl.SelectedIndex = 0;
      ctl.SelectedText = "none";
      ctl.Refresh();
    }
  }
}
