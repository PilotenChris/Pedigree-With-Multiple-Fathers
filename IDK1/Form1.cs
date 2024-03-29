using IDK1.FUserControl;
using PedigreeMF.FUserControl;

namespace IDK1 {
    public partial class Form1 : Form {
        bool tog = false;
        public Form1() {
            InitializeComponent();
            UC_ATP_Insert(new UC_Insert(this));
            //UC_ATP_Insert(new UC_Delete());
            toggleUCPD();
            toggleUCPD();
            updateColor(0);
        }
        public void UC_ATP_Insert(UserControl userControl) {
            var scpc = splitContainer1.Panel1.Controls;
            userControl.Dock = DockStyle.Fill;
            scpc.Clear();
            scpc.Add(userControl);
            userControl.BringToFront();
            userControl.Focus();
        }

        public void toggleUCPD() {
            if (tog) {
                tog = false;
                b_ToggleView.Text = "Database";
                UC_Pedigree();
            }
            else {
                tog = true;
                b_ToggleView.Text = "Pedigree";
                UC_Database();
            }
        }

        public void updateScreenSelected() {
            if (tog) {
                UC_Database();
            } else {
                UC_Pedigree();
            }
        }

        public void UC_Pedigree() {
            UC_Pedigree userControl2 = new UC_Pedigree();
            var scpc = splitContainer1.Panel2.Controls;
            userControl2.Dock = DockStyle.Fill;
            scpc.Clear();
            scpc.Add(userControl2);
            userControl2.BringToFront();
            userControl2.Focus();
        }

        public void UC_Database() {
            var scpc = splitContainer1.Panel2.Controls;
            var userControl = new UC_Database();
            userControl.Dock = DockStyle.Fill;
            scpc.Clear();
            scpc.Add(userControl);
            userControl.BringToFront();
            userControl.Focus();
        }

        private void b_Insert_Click(object sender, EventArgs e) {
            UC_Insert uc = new UC_Insert(this);
            UC_ATP_Insert(uc);
            updateColor(0);
        }

        private void b_Update_Click(object sender, EventArgs e) {
            UC_Update uc = new UC_Update(this);
            UC_ATP_Insert(uc);
            updateColor(1);
        }

        private void b_Delete_Click(object sender, EventArgs e) {
            UC_Delete uc = new UC_Delete(this);
            UC_ATP_Insert(uc);
            updateColor(2);
        }

        private void updateColor(int butId) {
            Button[] buttons = { b_Insert, b_Update, b_Delete };
            foreach (Button button in buttons) {
                button.ForeColor = Color.White;
                button.Font = new Font(button.Font, FontStyle.Regular);

            }
            buttons[butId].ForeColor = Color.FromArgb(200, 179, 214);
            buttons[butId].Font = new Font(buttons[butId].Font, FontStyle.Underline);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
            if (splitContainer1.SplitterDistance < 250) {
                splitContainer1.SplitterDistance = 250;
            }

        }

        private void b_Dummy_Click(object sender, EventArgs e) => SQLMethods.InsertDummy();

        private void b_ToggleView_Click(object sender, EventArgs e) {
            toggleUCPD();
        }

        private void b_Print_Click(object sender, EventArgs e) {
            UC_Pedigree userControl2 = new UC_Pedigree();
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.Filter = "PNG Image|*.png";
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string filePath = saveFileDialog.FileName;

                    userControl2.SaveCanvasAsPNG(filePath);

                    MessageBox.Show("Canvas saved as PNG successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
