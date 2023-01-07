using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;
using IDK1.FUserControl;

namespace IDK1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UC_ATP_Insert(new UC_Insert());
        }
        public void UC_ATP_Insert(UserControl userControl)
        {
            var scpc = splitContainer1.Panel1.Controls;
            userControl.Dock = DockStyle.Fill;
            scpc.Clear();
            scpc.Add(userControl);
            userControl.BringToFront();
            userControl.Focus();
        }

        private void b_Insert_Click(object sender, EventArgs e)
        {
            UC_Insert uc = new UC_Insert();
            UC_ATP_Insert(uc);
            
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer1.SplitterDistance < 250)
            {
                splitContainer1.SplitterDistance = 250;
            }
            
        }
    }
}