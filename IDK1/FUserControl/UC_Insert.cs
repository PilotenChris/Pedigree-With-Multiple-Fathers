﻿using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
#pragma warning disable IDE0007 // Use implicit type
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
namespace IDK1.FUserControl;
public partial class UC_Insert : UserControl {

    private Form1 parentForm;

    public UC_Insert(Form1 form) {
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
        if (ValidateString(textBox6.Text, label6, textBox6, false, "M") & ValidateString(textBox5.Text, label5, textBox5, false, "F")) {
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

    // validate all inputs before submitting to DB
    private void b_InsertData_Click(object sender, EventArgs e) {
        TB_ID.Focus();
        //Validate Parent fields
        if (ValidateString(TB_ID.Text, L_ID, TB_ID, false) && // ID
            ValidateString(textBox5.Text, label5, textBox5, true) && //F
            (ValidateString(textBox6.Text, label6, textBox6, true) && ValidateString(textBox5.Text, label5, textBox5, false, showErrorOnFailure: false) || string.IsNullOrEmpty(textBox6.Text) && listBox1.Items.Count == 0) && //M
            IsValidDate(textBox2.Text, textBox2) && // Birth date
            IsValidDate(textBox3.Text, textBox3, true) // Death date
            ) {
            Debug.WriteLine("testasdasdawftwegweg<asdgargERHer");
            if (!string.IsNullOrEmpty(SQLMethods.GetIDFromEntity(TB_ID.Text))) // Check if ID exists already
            {
                ErrorMessage("ID already exists.");
                return;
            }
            Debug.WriteLine("testasdasdawftwegweg<asdgargERHer");
            string ID;
            int SexId = comboBox2.SelectedIndex + 1;
            string BirthDate = textBox2.Text;
            string DeathDate = textBox3.Text;
            int ColorId = comboBox1.SelectedIndex + 1;
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
            Debug.WriteLine("testasdasdawftwegweg<asdgargERHer");
            //EVERY FIELD IS VALIDATED. JUST PUT IT TO THE DB OR SOMETHING IDK.
            if (string.IsNullOrEmpty(SQLMethods.GetIDFromEntity(ID))) {
                SQLMethods.InsertEntityData(ID, BirthDate, SexId, ColorId);
                if (!string.IsNullOrEmpty(textBox3.Text)) {
                    SQLMethods.InsertDeathData(ID, DeathDate);
                }
                if (!string.IsNullOrEmpty(textBox5.Text)) {
                    SQLMethods.InsertParentData(ID, MotherId);
                }
                foreach (string FatherId in FatherIds) {
                    SQLMethods.InsertParentData(ID, FatherId);
                }

                resetFields();
            }
            else {
                TB_ID.Focus();
                ErrorMessage("ID already exists.");
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
        listBox1.Items.Clear();
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

    private bool ValidateMotherNotEmptyIfFather() {
        if (string.IsNullOrEmpty(textBox6.Text) && listBox1.Items.Count == 0) {
            return true;
        }
        else {
            ErrorMessage("You cannot add a father without a Mother");
            textBox5.Focus();
            return false;
        }
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
        timer1.Stop();
        L_ErrorMessageField.Text = string.Empty;
    }

    private bool ValidateString(string str, Label lab, TextBox box, bool Empty = false, string allowed = "FMU", bool showErrorOnFailure = true) {
        // Check if string is empty or not allowed to be empty
        if (string.IsNullOrEmpty(str) && !Empty) {
            box.Focus();
            if (showErrorOnFailure) {
                ErrorMessage("The input at '" + lab.Text + "' cannot be empty");
            }
            return false;
        }
        // Check if string is empty and allowed to be empty
        else if (string.IsNullOrEmpty(str)) {
            return true;
        }
        // Check if first character is valid
        if (!allowed.Contains(str.ToUpper()[0]) && !char.IsNumber(str[0])) {
            box.Focus();
            if (showErrorOnFailure) {
                ErrorMessage("The first character of the '" + lab.Text + "' field must be a letter (" + string.Join(", ", allowed.ToCharArray()) + ") or a number.");
            }
            return false;
        }
        // Check if string has a number if first character is valid
        if (!char.IsDigit(str[0])) {
            if (str.Length <= 1) {
                box.Focus();
                if (showErrorOnFailure) {
                    ErrorMessage(lab.Text + " Field must have a number.");
                }
                return false;
            }
        }
        // Check if all characters after the first are numeric
        for (int i = 1; i < str.Length; i++) {
            if (!char.IsNumber(str[i])) {
                box.Focus();
                if (showErrorOnFailure) {
                    ErrorMessage("The characters following the first one in this field must be numeric: " + lab.Text);
                }
                return false;
            }
        }
        return true;
    }

    private void TB_ID_TextChanged(object sender, EventArgs e) {
        if (TB_ID.Text.Length > 0) {
            // Convert the text to uppercase
            string upperCaseText = TB_ID.Text.ToUpper();
            // Set the TextBox's text to the uppercase version
            TB_ID.Text = upperCaseText;
            // Set the cursor position to the end of the text
            TB_ID.SelectionStart = upperCaseText.Length;

            // smart select the intended sex
            if (TB_ID.Text[0] == 'M') {
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf("Male");
            }
            else if (TB_ID.Text[0] == 'F') {
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf("Female");
            }
            else if (TB_ID.Text[0] == 'U') {
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf("Unknown");
            }
        }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

    }

    private void L_ErrorMessageField_Click(object sender, EventArgs e) {

    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

    }
}
