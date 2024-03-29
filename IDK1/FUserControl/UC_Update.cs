﻿using System.Collections;
using System.Drawing.Drawing2D;
using System.Globalization;
using PedigreeMF.FUserControl;
#pragma warning disable IDE0007 // Use implicit type
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
namespace IDK1.FUserControl;
public partial class UC_Update : UserControl {

    private Form1 parentForm;

    public UC_Update(Form1 form) {
        // Initialize the user control
        InitializeComponent();

        this.parentForm = form;

        listbox1Update();

        // Get data from SQLMethods
        var colorData = SQLMethods.GetColorData();
        var sexData = SQLMethods.GetSexData();

        // Add data to combo boxes and select default + centering text

        comboBox1.Items.AddRange(colorData.ToArray());
        comboBox1.SelectedIndex = 0;
        comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
        comboBox1.DrawItem += comboBox1_DrawItem;


        comboBox2.Items.AddRange(sexData.ToArray());
        comboBox2.SelectedIndex = 0;
        comboBox2.DrawMode = DrawMode.OwnerDrawFixed;
        comboBox2.DrawItem += comboBox2_DrawItem;

    }

    // Event handler for Father Add button
    private void button1_Click(object sender, EventArgs e) {
        if (ValidateString(textBox6.Text, label6, textBox6)) {
            listBox1.Items.Add(textBox6.Text);
            textBox6.Clear();
        }

    }

    private void b_DeleteFather_Click(object sender, EventArgs e) {
        // Remove selected item from list box
        listBox1.Items.Remove(listBox1.SelectedItem);
        listbox1Update();
    }

    private void listbox1Update() {
        // Check if the list box has any items
        if (listBox1.Items.Count > 0) {
            textBox6.PlaceholderText = string.Empty;
        }
        else {
            textBox6.PlaceholderText = "Unknown";
        }
    }

    string PV_ID = "";
    ArrayList PV_Parents = new ArrayList();

    private void TB_ID_TextChanged(object sender, EventArgs e) {

        // Convert the text to uppercase
        string upperCaseText = TB_ID.Text.ToUpper();
        // Set the TextBox's text to the uppercase version
        TB_ID.Text = upperCaseText;
        // Set the cursor position to the end of the text
        TB_ID.SelectionStart = upperCaseText.Length;


        if (ValidateString(TB_ID.Text, L_ID, TB_ID, true) && !string.IsNullOrEmpty(TB_ID.Text) && PV_ID != TB_ID.Text) {
            if (!string.IsNullOrEmpty(SQLMethods.GetIDFromEntity(TB_ID.Text))) {
                listBox1.Items.Clear();
                PV_Parents.Clear();
                string id = TB_ID.Text;
                PV_ID = id;
                comboBox2.SelectedIndex = SQLMethods.GetSexFromEntity(id);
                textBox2.Text = SQLMethods.GetBirthFromEntity(id);
                textBox3.Text = SQLMethods.GetDeathFromEntity(id);
                comboBox1.SelectedIndex = SQLMethods.GetColorFromEntity(id);
                textBox5.Text = SQLMethods.GetMotherFromEntity(id);
                ArrayList fatherdata = SQLMethods.GetFatherFromEntity(id);
                PV_Parents.Add(textBox5.Text);
                PV_Parents.AddRange(fatherdata);
                listBox1.Items.AddRange(fatherdata.ToArray());
            }
        }
        else if (string.IsNullOrEmpty(TB_ID.Text)) { resetFields(); }
    }

    // validate all inputs before submitting to DB
    private void b_InsertData_Click(object sender, EventArgs e) {
        TB_ID.Focus();
        //Validate Parent fields
        if (ValidateString(TB_ID.Text, L_ID, TB_ID, false, false) && // ID
            ValidateString(textBox5.Text, label5, textBox5, true) && //F
            ValidateString(textBox6.Text, label6, textBox6, true) && //M
            IsValidDate(textBox2.Text, textBox2) && // Birth date
            IsValidDate(textBox3.Text, textBox3, true) /* Death date */) {

            string ID;
            string MotherId = "";
            HashSet<string> FatherIds = new HashSet<string>();

            if (char.IsNumber(TB_ID.Text[0])) ID = comboBox2.Text[0] + TB_ID.Text;
            else ID = TB_ID.Text;

            if (!string.IsNullOrEmpty(textBox5.Text)) {
                if (char.IsNumber(textBox5.Text[0])) MotherId = "F" + textBox5.Text;
                else MotherId = textBox5.Text;
            }
            if (textBox6.Text.Length > 0) {
                if (!textBox6.Text.StartsWith("M")) {
                    FatherIds.Add("M" + textBox6.Text);
                }
                else {
                    FatherIds.Add(textBox6.Text);
                }
            }
            foreach (string item in listBox1.Items) {
                if (!item.StartsWith("M")) {
                    FatherIds.Add("M" + item);
                }
                else {
                    FatherIds.Add(item);
                }
            }
            //EVERY FIELD IS VALIDATED. JUST PUT IT TO THE DB OR SOMETHING IDK.

            SQLMethods.UpdateSex(ID, comboBox2.SelectedIndex + 1);
            SQLMethods.UpdateBirth(ID, textBox2.Text);
            if (!string.IsNullOrEmpty(textBox3.Text)) {
                if (string.IsNullOrEmpty(SQLMethods.GetDeathFromEntity(ID))) {
                    SQLMethods.InsertDeathData(ID, textBox3.Text);
                }
                else {
                    SQLMethods.UpdateDeath(ID, textBox3.Text);
                }
            }
            else SQLMethods.DeleteDeath(ID);
            SQLMethods.UpdateColor(ID, comboBox1.SelectedIndex + 1);
            // Parents
            SQLMethods.DeleteAllParents(ID);
            if (!string.IsNullOrEmpty(textBox5.Text)) {
                SQLMethods.InsertParentData(ID, MotherId);
            }
            foreach (string FatherId in FatherIds) {
                SQLMethods.InsertParentData(ID, FatherId);
            }


            parentForm.updateScreenSelected();
        }
    }



    List<Control> controls = new List<Control>();
    private void resetFields() {
        GetControlsRecursive(this, controls);
        foreach (Control c in controls) {
            if (c.GetType() == typeof(TextBox)) {
                ((TextBox)c).Clear();
            }
            else if (c.GetType() == typeof(ComboBox)) {
                ((ComboBox)c).SelectedIndex = 0;
            }
        }
    }
    private static void GetControlsRecursive(Control container, List<Control> controls) {
        foreach (Control c in container.Controls) {
            controls.Add(c);
            if (c.Controls.Count > 0) {
                GetControlsRecursive(c, controls);
            }
        }
    }

    private void comboBox1_DrawItem(object sender, DrawItemEventArgs e) {
        // Get the item text
        string text = comboBox1.GetItemText(comboBox1.Items[e.Index]);

        // Get the bounds of the item
        Rectangle bounds = e.Bounds;

        // Get the font for the item
        Font font = e.Font;

        Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.Black : e.ForeColor;

        // Set the SmoothingMode and InterpolationMode properties of the Graphics object
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        // Get the TextFormatFlags for the item
        TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;

        // Draw the item text
        TextRenderer.DrawText(e.Graphics, text, font, bounds, textColor, flags);
    }
    private void comboBox2_DrawItem(object sender, DrawItemEventArgs e) {
        // Get the item text
        string text = comboBox2.GetItemText(comboBox2.Items[e.Index]);

        // Get the bounds of the item
        Rectangle bounds = e.Bounds;

        // Get the font for the item
        Font font = e.Font;

        Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.Black : e.ForeColor;

        // Set the SmoothingMode and InterpolationMode properties of the Graphics object
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        // Get the TextFormatFlags for the item
        TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;

        // Draw the item text
        TextRenderer.DrawText(e.Graphics, text, font, bounds, textColor, flags);
    }

    private bool IsValidDate(string input, TextBox box, bool bEmpty = false) {
        // Use DateTime.TryParseExact method to validate the format of the input string
#pragma warning disable IDE0059 // Unnecessary assignment of a value | breaks without
        if (!DateTime.TryParseExact(input, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)) {
            if (string.IsNullOrEmpty(input) && bEmpty) { return true; }
            box.Focus();
            ErrorMessage($"{input} is not a valid date. Use format YYYY/MM/DD.");
            return false;
        }
        else return true;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
    }

    private System.Windows.Forms.Timer timer1 = new();

    // Display error message and Set timer to remove error message
    private void ErrorMessage(string message) {
        L_ErrorMessageField.Text = message;
        if (timer1.Enabled) timer1.Dispose();
        timer1.Tick += new EventHandler(RemoveErrorMessage);
        timer1.Interval = 5000;
        timer1.Start();
    }

    // Stop timer and remove error message
    private void RemoveErrorMessage(object sender, EventArgs e) {
        if (timer1.Enabled) timer1.Stop();
        L_ErrorMessageField.Text = string.Empty;
    }

    private bool ValidateString(string str, Label lab, TextBox box, bool Empty = false, bool FirstIsNumber = true) {
        // Check if string is empty or not allowed to be empty
        if (string.IsNullOrEmpty(str) && !Empty) {
            box.Focus();
            ErrorMessage("The input at '" + lab.Text + "' cannot be empty");
            return false;
        }
        // Check if string is empty and allowed to be empty
        else if (string.IsNullOrEmpty(str)) {
            return true;
        }
        // Check if first character is valid
        if (str[0] != 'F' && str[0] != 'M' && str[0] != 'U' && (!char.IsNumber(str[0]) && FirstIsNumber)) {
            box.Focus();
            string errMessBuilder = "The first character of the '" + lab.Text + "' field must be a letter (F, M, or U)";
            if (FirstIsNumber) {
                errMessBuilder += " or a number.";
            }
            ErrorMessage(errMessBuilder);
            return false;
        }
        //else if (!FirstIsNumber) {
        //    box.Focus();
        //    ErrorMessage("The first character cannot be a number.");
        //    return false;
        //}
        // Check if string has a number if first character is valid
        if (!(str[0] != 'F' && str[0] != 'M' && str[0] != 'U')) {
            if (str.Length <= 1) {
                box.Focus();
                ErrorMessage(lab.Text + " Field must have a number.");
                return false;
            }
        }
        // Check if all characters after the first are numeric
        for (int i = 1; i < str.Length; i++) {
            if (!char.IsNumber(str[i])) {
                box.Focus();
                ErrorMessage("The characters following the first one in this field must be numeric: " + lab.Text);
                return false;
            }
        }
        return true;
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

    }

    private void UC_Update_Load(object sender, EventArgs e) {

    }

    private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e) {

    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

    }

    private void textBox2_TextChanged(object sender, EventArgs e) {

    }
}
