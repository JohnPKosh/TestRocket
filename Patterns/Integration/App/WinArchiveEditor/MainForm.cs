using ArchiveData.Models;
using ArchiveLogic.Generate;
using adata = Azos.Data;
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
  public partial class MainForm : Form
  {
    #region Class Initialization

    public MainForm()
    {
      InitializeComponent();
    }

    public object[] values { get; set; } 

    #endregion

    #region Generate Fake Logs

    private void btn_Generate_Click(object sender, EventArgs e)
    {      
      if (int.TryParse(txt_Quantity.Text, out int count))
      {
        trk_Index.Maximum = count-1;
        trk_Index.Value = 0;

        // For fake logs we need to `Select` the instance property. With other FakeRow implementors it is not necessary.
        values = FakeBuilder.GenerateMany<FakeLogMessage>(1, 1, (ulong)count).Select(x=> x.Instance).ToArray();
        txt_Item.Text = ((adata.TypedDoc)values[trk_Index.Value]).ToJsonDataMap().ToString();
      }
    }

    #endregion

    #region Navigate Fake Logs

    private void trk_Index_ValueChanged(object sender, EventArgs e)
    {
      if(values != null)
        txt_Item.Text = ((adata.TypedDoc)values[trk_Index.Value]).ToJsonDataMap().ToString();
    }

    private void btn_Next_Click(object sender, EventArgs e)
    {
      if (values != null && trk_Index.Value < trk_Index.Maximum)
      {
        trk_Index.Value++;
        txt_Item.Text = ((adata.TypedDoc)values[(int)trk_Index.Value]).ToJsonDataMap().ToString();
      }
    }

    private void btn_Prev_Click(object sender, EventArgs e)
    {
      if (values != null && trk_Index.Value > trk_Index.Minimum)
      {
        trk_Index.Value--;
        txt_Item.Text = ((adata.TypedDoc)values[(int)trk_Index.Value]).ToJsonDataMap().ToString();
      }
    }

    #endregion

    #region Open and Save

    private void btn_Save_Click(object sender, EventArgs e)
    {
      var savef = new SaveForm();
      savef.Show();      
    }

    private void btn_Open_Click(object sender, EventArgs e)
    {

    }

    #endregion
  }
}
