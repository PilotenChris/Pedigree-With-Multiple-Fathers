namespace IDK1 {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            tableLayoutPanel1 = new TableLayoutPanel();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            flowLayoutPanel1 = new FlowLayoutPanel();
            b_Insert = new Button();
            b_Update = new Button();
            b_Delete = new Button();
            b_Print = new Button();
            b_Dummy = new Button();
            b_ToggleView = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(splitContainer1, 0, 1);
            tableLayoutPanel1.Controls.Add(splitContainer2, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(950, 672);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.FromArgb(64, 64, 64);
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(3, 39);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Size = new Size(944, 630);
            splitContainer1.SplitterDistance = 250;
            splitContainer1.TabIndex = 99999;
            splitContainer1.TabStop = false;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(flowLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(b_Dummy);
            splitContainer2.Panel2.Controls.Add(b_ToggleView);
            splitContainer2.Size = new Size(944, 30);
            splitContainer2.SplitterDistance = 618;
            splitContainer2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(b_Insert);
            flowLayoutPanel1.Controls.Add(b_Update);
            flowLayoutPanel1.Controls.Add(b_Delete);
            flowLayoutPanel1.Controls.Add(b_Print);
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(615, 26);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // b_Insert
            // 
            b_Insert.FlatAppearance.BorderSize = 0;
            b_Insert.FlatStyle = FlatStyle.Flat;
            b_Insert.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            b_Insert.ForeColor = Color.White;
            b_Insert.Location = new Point(3, 3);
            b_Insert.Name = "b_Insert";
            b_Insert.Size = new Size(75, 23);
            b_Insert.TabIndex = 0;
            b_Insert.Text = "Insert";
            b_Insert.UseVisualStyleBackColor = true;
            b_Insert.Click += b_Insert_Click;
            // 
            // b_Update
            // 
            b_Update.FlatAppearance.BorderSize = 0;
            b_Update.FlatStyle = FlatStyle.Flat;
            b_Update.ForeColor = Color.White;
            b_Update.Location = new Point(84, 3);
            b_Update.Name = "b_Update";
            b_Update.Size = new Size(75, 23);
            b_Update.TabIndex = 1;
            b_Update.Text = "Update";
            b_Update.UseVisualStyleBackColor = true;
            b_Update.Click += b_Update_Click;
            // 
            // b_Delete
            // 
            b_Delete.FlatAppearance.BorderSize = 0;
            b_Delete.FlatStyle = FlatStyle.Flat;
            b_Delete.ForeColor = Color.White;
            b_Delete.Location = new Point(165, 3);
            b_Delete.Name = "b_Delete";
            b_Delete.Size = new Size(75, 23);
            b_Delete.TabIndex = 2;
            b_Delete.Text = "Delete";
            b_Delete.UseVisualStyleBackColor = true;
            b_Delete.Click += b_Delete_Click;
            // 
            // b_Print
            // 
            b_Print.FlatAppearance.BorderSize = 0;
            b_Print.FlatStyle = FlatStyle.Flat;
            b_Print.ForeColor = Color.White;
            b_Print.Location = new Point(246, 3);
            b_Print.Name = "b_Print";
            b_Print.Size = new Size(75, 23);
            b_Print.TabIndex = 3;
            b_Print.Text = "Print";
            b_Print.UseVisualStyleBackColor = true;
            b_Print.Click += b_Print_Click;
            // 
            // b_Dummy
            // 
            b_Dummy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            b_Dummy.Location = new Point(140, 3);
            b_Dummy.Name = "b_Dummy";
            b_Dummy.Size = new Size(75, 23);
            b_Dummy.TabIndex = 1;
            b_Dummy.Text = "Dummy";
            b_Dummy.UseVisualStyleBackColor = true;
            b_Dummy.Click += b_Dummy_Click;
            // 
            // b_ToggleView
            // 
            b_ToggleView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            b_ToggleView.Location = new Point(221, 3);
            b_ToggleView.Name = "b_ToggleView";
            b_ToggleView.Size = new Size(98, 23);
            b_ToggleView.TabIndex = 0;
            b_ToggleView.Text = "button5";
            b_ToggleView.UseVisualStyleBackColor = true;
            b_ToggleView.Click += b_ToggleView_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(974, 689);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(650, 600);
            Name = "Form1";
            Text = "Pedigree";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button b_Insert;
        private Button b_Update;
        private Button b_Delete;
        private Button b_Print;
        private Button b_ToggleView;
        private Button b_Dummy;
    }
}
