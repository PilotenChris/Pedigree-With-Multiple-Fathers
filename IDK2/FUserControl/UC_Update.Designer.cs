namespace IDK1.FUserControl;

partial class UC_Update {
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
        tableLayoutPanel1 = new TableLayoutPanel();
        label6 = new Label();
        splitContainer2 = new SplitContainer();
        listBox1 = new ListBox();
        b_DeleteFather = new Button();
        panel7 = new Panel();
        splitContainer1 = new SplitContainer();
        textBox6 = new TextBox();
        button1 = new Button();
        label5 = new Label();
        panel6 = new Panel();
        textBox5 = new TextBox();
        label4 = new Label();
        panel5 = new Panel();
        comboBox1 = new ComboBox();
        label3 = new Label();
        panel4 = new Panel();
        textBox3 = new TextBox();
        label2 = new Label();
        panel3 = new Panel();
        textBox2 = new TextBox();
        label1 = new Label();
        panel2 = new Panel();
        comboBox2 = new ComboBox();
        panel1 = new Panel();
        TB_ID = new TextBox();
        L_ID = new Label();
        b_InsertData = new Button();
        L_ErrorMessageField = new Label();
        panel8 = new Panel();
        tableLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
        splitContainer2.Panel1.SuspendLayout();
        splitContainer2.Panel2.SuspendLayout();
        splitContainer2.SuspendLayout();
        panel7.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        panel6.SuspendLayout();
        panel5.SuspendLayout();
        panel4.SuspendLayout();
        panel3.SuspendLayout();
        panel2.SuspendLayout();
        panel1.SuspendLayout();
        panel8.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.15267F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.84733F));
        tableLayoutPanel1.Controls.Add(label6, 0, 6);
        tableLayoutPanel1.Controls.Add(splitContainer2, 1, 7);
        tableLayoutPanel1.Controls.Add(panel7, 0, 6);
        tableLayoutPanel1.Controls.Add(label5, 0, 5);
        tableLayoutPanel1.Controls.Add(panel6, 1, 5);
        tableLayoutPanel1.Controls.Add(label4, 0, 4);
        tableLayoutPanel1.Controls.Add(panel5, 1, 4);
        tableLayoutPanel1.Controls.Add(label3, 0, 3);
        tableLayoutPanel1.Controls.Add(panel4, 1, 3);
        tableLayoutPanel1.Controls.Add(label2, 0, 2);
        tableLayoutPanel1.Controls.Add(panel3, 1, 2);
        tableLayoutPanel1.Controls.Add(label1, 0, 1);
        tableLayoutPanel1.Controls.Add(panel2, 1, 1);
        tableLayoutPanel1.Controls.Add(panel1, 1, 0);
        tableLayoutPanel1.Controls.Add(L_ID, 0, 0);
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.MaximumSize = new Size(0, 350);
        tableLayoutPanel1.MinimumSize = new Size(239, 350);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 8;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.6275F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.62749F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.62749F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.62749F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.62749F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.62749F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.62981F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 18.60522F));
        tableLayoutPanel1.Size = new Size(239, 350);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // label6
        // 
        label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        label6.AutoSize = true;
        label6.Location = new Point(3, 250);
        label6.Margin = new Padding(3, 10, 3, 0);
        label6.Name = "label6";
        label6.Size = new Size(66, 15);
        label6.TabIndex = 13;
        label6.Text = "Father/s";
        label6.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // splitContainer2
        // 
        splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        splitContainer2.IsSplitterFixed = true;
        splitContainer2.Location = new Point(75, 283);
        splitContainer2.Name = "splitContainer2";
        // 
        // splitContainer2.Panel1
        // 
        splitContainer2.Panel1.Controls.Add(listBox1);
        splitContainer2.Panel1.Paint += splitContainer2_Panel1_Paint;
        // 
        // splitContainer2.Panel2
        // 
        splitContainer2.Panel2.Controls.Add(b_DeleteFather);
        splitContainer2.Size = new Size(161, 64);
        splitContainer2.SplitterDistance = 96;
        splitContainer2.TabIndex = 15;
        // 
        // listBox1
        // 
        listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        listBox1.FormattingEnabled = true;
        listBox1.HorizontalScrollbar = true;
        listBox1.ItemHeight = 15;
        listBox1.Location = new Point(3, 3);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(163, 64);
        listBox1.TabIndex = 4;
        listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        // 
        // b_DeleteFather
        // 
        b_DeleteFather.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        b_DeleteFather.ForeColor = Color.Black;
        b_DeleteFather.Location = new Point(5, 3);
        b_DeleteFather.Name = "b_DeleteFather";
        b_DeleteFather.Size = new Size(52, 23);
        b_DeleteFather.TabIndex = 5;
        b_DeleteFather.Text = "Delete";
        b_DeleteFather.UseVisualStyleBackColor = true;
        b_DeleteFather.Click += b_DeleteFather_Click;
        // 
        // panel7
        // 
        panel7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel7.Controls.Add(splitContainer1);
        panel7.Location = new Point(75, 243);
        panel7.Name = "panel7";
        panel7.Size = new Size(161, 34);
        panel7.TabIndex = 14;
        // 
        // splitContainer1
        // 
        splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        splitContainer1.IsSplitterFixed = true;
        splitContainer1.Location = new Point(0, 3);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(textBox6);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(button1);
        splitContainer1.Size = new Size(161, 28);
        splitContainer1.SplitterDistance = 98;
        splitContainer1.TabIndex = 0;
        // 
        // textBox6
        // 
        textBox6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        textBox6.Location = new Point(3, 4);
        textBox6.Name = "textBox6";
        textBox6.PlaceholderText = "Unkown";
        textBox6.Size = new Size(92, 23);
        textBox6.TabIndex = 1;
        textBox6.TextAlign = HorizontalAlignment.Center;
        // 
        // button1
        // 
        button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        button1.ForeColor = Color.Black;
        button1.Location = new Point(2, 4);
        button1.MaximumSize = new Size(0, 23);
        button1.MinimumSize = new Size(0, 23);
        button1.Name = "button1";
        button1.Size = new Size(53, 23);
        button1.TabIndex = 0;
        button1.Text = "Add";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label5
        // 
        label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        label5.AutoSize = true;
        label5.Location = new Point(3, 210);
        label5.Margin = new Padding(3, 10, 3, 0);
        label5.Name = "label5";
        label5.Size = new Size(66, 15);
        label5.TabIndex = 11;
        label5.Text = "Mother";
        label5.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panel6
        // 
        panel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel6.Controls.Add(textBox5);
        panel6.Location = new Point(75, 203);
        panel6.Name = "panel6";
        panel6.Size = new Size(161, 34);
        panel6.TabIndex = 12;
        // 
        // textBox5
        // 
        textBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        textBox5.Location = new Point(3, 3);
        textBox5.Name = "textBox5";
        textBox5.PlaceholderText = "Unknown";
        textBox5.Size = new Size(155, 23);
        textBox5.TabIndex = 1;
        textBox5.TextAlign = HorizontalAlignment.Center;
        // 
        // label4
        // 
        label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        label4.AutoSize = true;
        label4.Location = new Point(3, 170);
        label4.Margin = new Padding(3, 10, 3, 0);
        label4.Name = "label4";
        label4.Size = new Size(66, 15);
        label4.TabIndex = 9;
        label4.Text = "Color";
        label4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panel5
        // 
        panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel5.Controls.Add(comboBox1);
        panel5.Location = new Point(75, 163);
        panel5.Name = "panel5";
        panel5.Size = new Size(161, 34);
        panel5.TabIndex = 10;
        // 
        // comboBox1
        // 
        comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new Point(3, 3);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(154, 23);
        comboBox1.TabIndex = 0;
        comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        label3.AutoSize = true;
        label3.Location = new Point(3, 130);
        label3.Margin = new Padding(3, 10, 3, 0);
        label3.Name = "label3";
        label3.Size = new Size(66, 15);
        label3.TabIndex = 7;
        label3.Text = "Death Date";
        label3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panel4
        // 
        panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel4.Controls.Add(textBox3);
        panel4.Location = new Point(75, 123);
        panel4.Name = "panel4";
        panel4.Size = new Size(161, 34);
        panel4.TabIndex = 8;
        // 
        // textBox3
        // 
        textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        textBox3.Location = new Point(3, 3);
        textBox3.Name = "textBox3";
        textBox3.PlaceholderText = "YYYY/MM/DD";
        textBox3.Size = new Size(155, 23);
        textBox3.TabIndex = 1;
        textBox3.TextAlign = HorizontalAlignment.Center;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        label2.AutoSize = true;
        label2.Location = new Point(3, 90);
        label2.Margin = new Padding(3, 10, 3, 0);
        label2.Name = "label2";
        label2.Size = new Size(66, 15);
        label2.TabIndex = 5;
        label2.Text = "Birth Date";
        label2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panel3
        // 
        panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel3.Controls.Add(textBox2);
        panel3.Location = new Point(75, 83);
        panel3.Name = "panel3";
        panel3.Size = new Size(161, 34);
        panel3.TabIndex = 6;
        // 
        // textBox2
        // 
        textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        textBox2.Location = new Point(3, 3);
        textBox2.Name = "textBox2";
        textBox2.PlaceholderText = "YYYY/MM/DD";
        textBox2.Size = new Size(155, 23);
        textBox2.TabIndex = 1;
        textBox2.TextAlign = HorizontalAlignment.Center;
        textBox2.TextChanged += textBox2_TextChanged;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        label1.AutoSize = true;
        label1.Location = new Point(3, 50);
        label1.Margin = new Padding(3, 10, 3, 0);
        label1.Name = "label1";
        label1.Size = new Size(66, 15);
        label1.TabIndex = 3;
        label1.Text = "Sex";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panel2
        // 
        panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel2.Controls.Add(comboBox2);
        panel2.Location = new Point(75, 43);
        panel2.Name = "panel2";
        panel2.Size = new Size(161, 34);
        panel2.TabIndex = 4;
        // 
        // comboBox2
        // 
        comboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new Point(3, 4);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new Size(154, 23);
        comboBox2.TabIndex = 1;
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel1.Controls.Add(TB_ID);
        panel1.Location = new Point(75, 3);
        panel1.Name = "panel1";
        panel1.Size = new Size(161, 34);
        panel1.TabIndex = 2;
        // 
        // TB_ID
        // 
        TB_ID.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        TB_ID.Location = new Point(3, 3);
        TB_ID.Name = "TB_ID";
        TB_ID.Size = new Size(155, 23);
        TB_ID.TabIndex = 1;
        TB_ID.TextAlign = HorizontalAlignment.Center;
        TB_ID.TextChanged += TB_ID_TextChanged;
        // 
        // L_ID
        // 
        L_ID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        L_ID.AutoSize = true;
        L_ID.Location = new Point(3, 10);
        L_ID.Margin = new Padding(3, 10, 3, 0);
        L_ID.Name = "L_ID";
        L_ID.Size = new Size(66, 15);
        L_ID.TabIndex = 0;
        L_ID.Text = "ID";
        L_ID.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // b_InsertData
        // 
        b_InsertData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        b_InsertData.ForeColor = Color.Black;
        b_InsertData.Location = new Point(3, 356);
        b_InsertData.Name = "b_InsertData";
        b_InsertData.Size = new Size(233, 23);
        b_InsertData.TabIndex = 2;
        b_InsertData.Text = "Update";
        b_InsertData.UseVisualStyleBackColor = true;
        b_InsertData.Click += b_InsertData_Click;
        // 
        // L_ErrorMessageField
        // 
        L_ErrorMessageField.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        L_ErrorMessageField.Location = new Point(3, 0);
        L_ErrorMessageField.Name = "L_ErrorMessageField";
        L_ErrorMessageField.Size = new Size(227, 100);
        L_ErrorMessageField.TabIndex = 3;
        // 
        // panel8
        // 
        panel8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panel8.Controls.Add(L_ErrorMessageField);
        panel8.Location = new Point(3, 385);
        panel8.Name = "panel8";
        panel8.Size = new Size(233, 100);
        panel8.TabIndex = 4;
        // 
        // UC_Update
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(64, 64, 64);
        Controls.Add(panel8);
        Controls.Add(b_InsertData);
        Controls.Add(tableLayoutPanel1);
        ForeColor = Color.White;
        MinimumSize = new Size(239, 0);
        Name = "UC_Update";
        Size = new Size(239, 639);
        Load += UC_Update_Load;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        splitContainer2.Panel1.ResumeLayout(false);
        splitContainer2.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
        splitContainer2.ResumeLayout(false);
        panel7.ResumeLayout(false);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel1.PerformLayout();
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        panel6.ResumeLayout(false);
        panel6.PerformLayout();
        panel5.ResumeLayout(false);
        panel4.ResumeLayout(false);
        panel4.PerformLayout();
        panel3.ResumeLayout(false);
        panel3.PerformLayout();
        panel2.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        panel8.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Label L_ID;
    private TextBox TB_ID;
    private Panel panel1;
    private Label label5;
    private Panel panel6;
    private TextBox textBox5;
    private Label label4;
    private Panel panel5;
    private Label label3;
    private Panel panel4;
    private TextBox textBox3;
    private Label label2;
    private Panel panel3;
    private TextBox textBox2;
    private Label label1;
    private Panel panel2;
    private Label label6;
    private Panel panel7;
    private TextBox textBox6;
    private SplitContainer splitContainer1;
    private Button button1;
    private ComboBox comboBox1;
    private ComboBox comboBox2;
    private Button b_InsertData;
    private ListBox listBox1;
    private Button b_DeleteFather;
    private SplitContainer splitContainer2;
    private Label L_ErrorMessageField;
    private Panel panel8;
}
