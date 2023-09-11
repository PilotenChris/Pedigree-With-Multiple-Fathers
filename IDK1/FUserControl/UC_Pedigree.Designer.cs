namespace IDK1.FUserControl;

partial class UC_Pedigree {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        panel1 = new Panel();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.AutoScroll = true;
        panel1.BorderStyle = BorderStyle.FixedSingle;
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(981, 650);
        panel1.TabIndex = 0;
        // 
        // UC_Pedigree
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(64, 64, 64);
        Controls.Add(panel1);
        Name = "UC_Pedigree";
        Size = new Size(981, 650);
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
}
