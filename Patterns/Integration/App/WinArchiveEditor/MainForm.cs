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
      var peeps = FakeBuilder.GenerateMany<FakeLogMessage>(1, 1, 10_000).ToArray();

      if (peeps.Any()) MessageBox.Show(peeps[0].Instance.ToJsonDataMap().ToString());

      //MessageBox.Show($"generated my peeps and got {peeps.Length} random people");


    }
  }
}
