namespace IDK1.FUserControl;

partial class UC_Delete
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.L_ID = new System.Windows.Forms.Label();
            this.TB_ID = new System.Windows.Forms.TextBox();
            this.b_DeleteData = new System.Windows.Forms.Button();
            this.L_ErrorMessageField = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57142F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.Controls.Add(this.L_ID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TB_ID, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.b_DeleteData, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(172, 101);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // L_ID
            // 
            this.L_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.L_ID.Location = new System.Drawing.Point(52, 0);
            this.L_ID.Name = "L_ID";
            this.L_ID.Size = new System.Drawing.Size(67, 33);
            this.L_ID.TabIndex = 0;
            this.L_ID.Text = "ID";
            this.L_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TB_ID
            // 
            this.TB_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ID.ForeColor = System.Drawing.Color.Black;
            this.TB_ID.Location = new System.Drawing.Point(52, 36);
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(67, 23);
            this.TB_ID.TabIndex = 1;
            this.TB_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // b_DeleteData
            // 
            this.b_DeleteData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_DeleteData.ForeColor = System.Drawing.Color.Black;
            this.b_DeleteData.Location = new System.Drawing.Point(52, 69);
            this.b_DeleteData.Name = "b_DeleteData";
            this.b_DeleteData.Size = new System.Drawing.Size(67, 23);
            this.b_DeleteData.TabIndex = 2;
            this.b_DeleteData.Text = "Delete";
            this.b_DeleteData.UseVisualStyleBackColor = true;
            this.b_DeleteData.Click += new System.EventHandler(this.b_DeleteData_Click);
            // 
            // L_ErrorMessageField
            // 
            this.L_ErrorMessageField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.L_ErrorMessageField.Location = new System.Drawing.Point(3, 107);
            this.L_ErrorMessageField.Name = "L_ErrorMessageField";
            this.L_ErrorMessageField.Size = new System.Drawing.Size(172, 146);
            this.L_ErrorMessageField.TabIndex = 1;
            // 
            // UC_Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.L_ErrorMessageField);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(178, 101);
            this.Name = "UC_Delete";
            this.Size = new System.Drawing.Size(178, 646);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Label L_ID;
    private TextBox TB_ID;
    private Button b_DeleteData;
    private TableLayoutPanel tableLayoutPanel2;
    private Label L_ErrorMessageField;
}
