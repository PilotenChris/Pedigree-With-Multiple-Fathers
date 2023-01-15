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
            updateColor(0);
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
            updateColor(0);
        }

        private void b_Update_Click(object sender, EventArgs e)
        {
            UC_Update uc = new UC_Update();
            UC_ATP_Insert(uc);
            updateColor(1);
        }

        private void b_Delete_Click(object sender, EventArgs e)
        {
            UC_Delete uc = new UC_Delete();
            UC_ATP_Insert(uc);
            updateColor(2);
        }
        
        private void updateColor(int butId)
        {
            Button[] buttons= {b_Insert,b_Update,b_Delete};
            foreach(Button button in buttons)
            {
                button.ForeColor = Color.White;
                button.Font = new Font(button.Font, FontStyle.Regular);

            }
            buttons[butId].ForeColor = Color.FromArgb(200, 179, 214);
            buttons[butId].Font = new Font(buttons[butId].Font, FontStyle.Underline);
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