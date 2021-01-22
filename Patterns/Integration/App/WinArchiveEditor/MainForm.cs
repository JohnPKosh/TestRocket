using ArchiveData.Models;
using ArchiveLogic.Generate;
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
    public MainForm()
    {
      InitializeComponent();
    }

    private void btn_Generate_Click(object sender, EventArgs e)
    {
      //var count = txt_Quantity.Text;
      if (int.TryParse(txt_Quantity.Text, out int count))
      {
        var peeps = FakeBuilder.GenerateMany<FakeLogMessage>(1, 1, (ulong)count).Select(x=> x.Instance).ToArray();
        txt_Item.Text = peeps[0].ToJsonDataMap().ToString();
        //if (peeps.Any()) MessageBox.Show(peeps[0].ToJsonDataMap().ToString());
      }

      

      //MessageBox.Show($"generated my peeps and got {peeps.Length} random people");


    }
  }
}
