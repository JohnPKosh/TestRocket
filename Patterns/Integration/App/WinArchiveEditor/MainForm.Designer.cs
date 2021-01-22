
namespace WinArchiveEditor
{
  partial class MainForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btn_Generate = new System.Windows.Forms.Button();
      this.txt_Quantity = new System.Windows.Forms.TextBox();
      this.lbl_Quantity = new System.Windows.Forms.Label();
      this.txt_Item = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btn_Generate
      // 
      this.btn_Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Generate.Location = new System.Drawing.Point(676, 404);
      this.btn_Generate.Name = "btn_Generate";
      this.btn_Generate.Size = new System.Drawing.Size(112, 34);
      this.btn_Generate.TabIndex = 0;
      this.btn_Generate.Text = "Generate";
      this.btn_Generate.UseVisualStyleBackColor = true;
      this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
      // 
      // txt_Quantity
      // 
      this.txt_Quantity.Location = new System.Drawing.Point(12, 37);
      this.txt_Quantity.Name = "txt_Quantity";
      this.txt_Quantity.Size = new System.Drawing.Size(150, 31);
      this.txt_Quantity.TabIndex = 1;
      this.txt_Quantity.Text = "100";
      // 
      // lbl_Quantity
      // 
      this.lbl_Quantity.AutoSize = true;
      this.lbl_Quantity.Location = new System.Drawing.Point(12, 9);
      this.lbl_Quantity.Name = "lbl_Quantity";
      this.lbl_Quantity.Size = new System.Drawing.Size(180, 25);
      this.lbl_Quantity.TabIndex = 2;
      this.lbl_Quantity.Text = "Quantity to generate:";
      // 
      // txt_Item
      // 
      this.txt_Item.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txt_Item.Location = new System.Drawing.Point(12, 85);
      this.txt_Item.Multiline = true;
      this.txt_Item.Name = "txt_Item";
      this.txt_Item.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txt_Item.Size = new System.Drawing.Size(776, 302);
      this.txt_Item.TabIndex = 3;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.txt_Item);
      this.Controls.Add(this.lbl_Quantity);
      this.Controls.Add(this.txt_Quantity);
      this.Controls.Add(this.btn_Generate);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Win Archive Editor";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btn_Generate;
    private System.Windows.Forms.TextBox txt_Quantity;
    private System.Windows.Forms.Label lbl_Quantity;
    private System.Windows.Forms.TextBox txt_Item;
  }
}

