using IDK1.FUserControl;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace IDK1;
internal class Utils {
    public static bool ShowDialog(string text, string caption) {
        Form prompt = new Form() {
            BackColor= Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85))))),
            AutoSize=true,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = caption,
            StartPosition = FormStartPosition.CenterParent
        };

        Label textLabel = new Label() { Padding= new Padding(10), Text = text, AutoSize=true};

        FlowLayoutPanel buttonPanel = new FlowLayoutPanel() {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Bottom,
            Padding = new Padding(10),
            Height = 70,
            Margin = new Padding(0, 0, 0, 0),
        };


        Button bno = new Button() { Text = "No", BackColor = Color.White, DialogResult = DialogResult.No };
        Button byes = new Button() { Text = "Yes", BackColor = Color.White, DialogResult = DialogResult.Yes };
        Button bRememberYes = new Button() { Text = "Always Yes", BackColor = Color.White , DialogResult = DialogResult.Yes };

        string filename = "Properties/Preferences.json";
        string filePath = Path.Combine(Environment.CurrentDirectory, filename);

        string Jstring = File.ReadAllText(filePath);

        JObject obj = JObject.Parse(Jstring);

        if (obj["RememberYes"] != null ) {
            if ((bool)obj["RememberYes"]) {
                prompt.Close();
                return true;
            }
        }
        else {
            obj["RememberYes"] = false;
        }

        bRememberYes.Click += (sender, e) => {
            obj["RememberYes"] = true;
            File.WriteAllText(filePath, obj.ToString());
            prompt.Close();
        };
        byes.Click += (sender, e) => { prompt.Close(); };
        bno.Click += (sender, e) => { prompt.Close(); };

        buttonPanel.Controls.Add(bno);
        buttonPanel.Controls.Add(byes);
        buttonPanel.Controls.Add(bRememberYes);
        
        prompt.Controls.Add(textLabel);
        prompt.Controls.Add(buttonPanel);
        buttonPanel.Location = new Point((buttonPanel.Parent.ClientSize.Width - buttonPanel.Width) / 2, buttonPanel.Location.Y);
        prompt.AcceptButton = byes;


        return prompt.ShowDialog() == DialogResult.Yes;
    }
}
