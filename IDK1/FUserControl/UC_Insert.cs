using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDK1;

namespace IDK1.FUserControl;
public partial class UC_Insert : UserControl
{
    public UC_Insert()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        listBox1.Items.Add(textBox6.Text);
    }

    private void b_DeleteFather_Click(object sender, EventArgs e)
    {
        listBox1.Items.Remove(listBox1.SelectedItem);
    }

    private void b_InsertData_Click(object sender, EventArgs e)
    {
        
    }

    
}
