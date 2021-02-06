
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
      this.trk_Index = new System.Windows.Forms.TrackBar();
      this.btn_Next = new System.Windows.Forms.Button();
      this.btn_Prev = new System.Windows.Forms.Button();
      this.btn_Save = new System.Windows.Forms.Button();
      this.btn_Open = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.trk_Index)).BeginInit();
      this.SuspendLayout();
      // 
      // btn_Generate
      // 
      this.btn_Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Generate.Location = new System.Drawing.Point(1675, 1082);
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
      this.txt_Item.Size = new System.Drawing.Size(1979, 980);
      this.txt_Item.TabIndex = 3;
      // 
      // trk_Index
      // 
      this.trk_Index.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.trk_Index.Location = new System.Drawing.Point(198, 9);
      this.trk_Index.Name = "trk_Index";
      this.trk_Index.Size = new System.Drawing.Size(1793, 69);
      this.trk_Index.TabIndex = 4;
      this.trk_Index.ValueChanged += new System.EventHandler(this.trk_Index_ValueChanged);
      // 
      // btn_Next
      // 
      this.btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Next.Location = new System.Drawing.Point(1585, 1082);
      this.btn_Next.Name = "btn_Next";
      this.btn_Next.Size = new System.Drawing.Size(75, 34);
      this.btn_Next.TabIndex = 5;
      this.btn_Next.Text = "Next";
      this.btn_Next.UseVisualStyleBackColor = true;
      this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
      // 
      // btn_Prev
      // 
      this.btn_Prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Prev.Location = new System.Drawing.Point(1494, 1082);
      this.btn_Prev.Name = "btn_Prev";
      this.btn_Prev.Size = new System.Drawing.Size(75, 34);
      this.btn_Prev.TabIndex = 6;
      this.btn_Prev.Text = "Prev";
      this.btn_Prev.UseVisualStyleBackColor = true;
      this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
      // 
      // btn_Save
      // 
      this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Save.Location = new System.Drawing.Point(1804, 1082);
      this.btn_Save.Name = "btn_Save";
      this.btn_Save.Size = new System.Drawing.Size(92, 34);
      this.btn_Save.TabIndex = 7;
      this.btn_Save.Text = "Save As";
      this.btn_Save.UseVisualStyleBackColor = true;
      this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
      // 
      // btn_Open
      // 
      this.btn_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Open.Location = new System.Drawing.Point(1913, 1082);
      this.btn_Open.Name = "btn_Open";
      this.btn_Open.Size = new System.Drawing.Size(78, 34);
      this.btn_Open.TabIndex = 8;
      this.btn_Open.Text = "Open";
      this.btn_Open.UseVisualStyleBackColor = true;
      this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(2003, 1128);
      this.Controls.Add(this.btn_Open);
      this.Controls.Add(this.btn_Save);
      this.Controls.Add(this.btn_Prev);
      this.Controls.Add(this.btn_Next);
      this.Controls.Add(this.trk_Index);
      this.Controls.Add(this.txt_Item);
      this.Controls.Add(this.lbl_Quantity);
      this.Controls.Add(this.txt_Quantity);
      this.Controls.Add(this.btn_Generate);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Win Archive Editor";
      ((System.ComponentModel.ISupportInitialize)(this.trk_Index)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btn_Generate;
    private System.Windows.Forms.TextBox txt_Quantity;
    private System.Windows.Forms.Label lbl_Quantity;
    private System.Windows.Forms.TextBox txt_Item;
    private System.Windows.Forms.TrackBar trk_Index;
    private System.Windows.Forms.Button btn_Next;
    private System.Windows.Forms.Button btn_Prev;
    private System.Windows.Forms.Button btn_Save;
    private System.Windows.Forms.Button btn_Open;
  }
}

