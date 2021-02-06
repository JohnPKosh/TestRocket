
namespace WinArchiveEditor
{
  partial class SaveForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.grp_CompressOptions = new System.Windows.Forms.GroupBox();
      this.cmb_CompressionType = new System.Windows.Forms.ComboBox();
      this.btn_Save = new System.Windows.Forms.Button();
      this.grp_CompressOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // grp_CompressOptions
      // 
      this.grp_CompressOptions.Controls.Add(this.cmb_CompressionType);
      this.grp_CompressOptions.Location = new System.Drawing.Point(13, 13);
      this.grp_CompressOptions.Name = "grp_CompressOptions";
      this.grp_CompressOptions.Size = new System.Drawing.Size(201, 72);
      this.grp_CompressOptions.TabIndex = 0;
      this.grp_CompressOptions.TabStop = false;
      this.grp_CompressOptions.Text = "Compression Options";
      // 
      // cmb_CompressionType
      // 
      this.cmb_CompressionType.FormattingEnabled = true;
      this.cmb_CompressionType.Items.AddRange(new object[] {
            "none",
            "gzip",
            "gzip-max"});
      this.cmb_CompressionType.Location = new System.Drawing.Point(6, 30);
      this.cmb_CompressionType.Name = "cmb_CompressionType";
      this.cmb_CompressionType.Size = new System.Drawing.Size(182, 33);
      this.cmb_CompressionType.TabIndex = 1;
      // 
      // btn_Save
      // 
      this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_Save.Location = new System.Drawing.Point(676, 404);
      this.btn_Save.Name = "btn_Save";
      this.btn_Save.Size = new System.Drawing.Size(112, 34);
      this.btn_Save.TabIndex = 1;
      this.btn_Save.Text = "Save";
      this.btn_Save.UseVisualStyleBackColor = true;
      // 
      // SaveForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.btn_Save);
      this.Controls.Add(this.grp_CompressOptions);
      this.Name = "SaveForm";
      this.Text = "SaveForm";
      this.grp_CompressOptions.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox grp_CompressOptions;
    private System.Windows.Forms.ComboBox cmb_CompressionType;
    private System.Windows.Forms.Button btn_Save;
  }
}