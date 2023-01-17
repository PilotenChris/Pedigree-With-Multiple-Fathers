using System.Collections;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;
#pragma warning disable IDE0007 // Use implicit type
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
namespace IDK1.FUserControl;
public partial class UC_Update : UserControl
{
    public UC_Update()
    {
        // Initialize the user control
        InitializeComponent();

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
    private void button1_Click(object sender, EventArgs e)
    {
        if (ValidateString(textBox6.Text, label6, textBox6))
        {
            listBox1.Items.Add(textBox6.Text);
            textBox6.Clear();
        }

    }

    private void b_DeleteFather_Click(object sender, EventArgs e)
    {
        // Remove selected item from list box
        listBox1.Items.Remove(listBox1.SelectedItem);
        listbox1Update();
    }

    private void listbox1Update()
    {
        // Check if the list box has any items
        if (listBox1.Items.Count > 0)
        {
            textBox6.PlaceholderText = string.Empty;
        }
        else
        {
            textBox6.PlaceholderText = "Unknown";
        }
    }

    // validate all inputs before submitting to DB
    private void b_InsertData_Click(object sender, EventArgs e)
    {
        TB_ID.Focus();
        //Validate Parent fields
        if (ValidateString(TB_ID.Text, L_ID, TB_ID, false) && // ID
            ValidateString(textBox5.Text, label5, textBox5, true) && //F
            ValidateString(textBox6.Text, label6, textBox6, true) && //M
            IsValidDate(textBox2.Text, textBox2) && // Birth date
            IsValidDate(textBox3.Text, textBox3, true) // Death date
            )
        {
            ErrorMessage("Success");

            string ID;
            int SexId = comboBox2.SelectedIndex + 1;
            string BirthDate = textBox2.Text;
            string DeathDate = textBox3.Text;
            int ColorId = comboBox1.SelectedIndex + 1;
            string MotherId = "";
            HashSet<string> FatherIds = new HashSet<string>();

            if (char.IsNumber(TB_ID.Text[0])) ID = comboBox2.Text[0] + TB_ID.Text;
            else ID = TB_ID.Text;

            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                if (char.IsNumber(textBox5.Text[0])) MotherId = "F" + textBox5.Text;
                else MotherId = textBox5.Text;
            }
            if (textBox6.Text.Length > 0)
            {
                if (textBox6.Text.StartsWith("M"))
                {
                    FatherIds.Add("M" + textBox6.Text);
                }
                else
                {
                    FatherIds.Add(textBox6.Text);
                }
            }
            foreach (string item in listBox1.Items)
            {
                if (!item.StartsWith("M"))
                {
                    FatherIds.Add("M" + item);
                }
                else
                {
                    FatherIds.Add(item);
                }
            }
            //EVERY FIELD IS VALIDATED. JUST PUT IT TO THE DB OR SOMETHING IDK.

            SQLMethods.InsertEntityData(ID, BirthDate, SexId, ColorId);
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                SQLMethods.InsertDeathData(ID, DeathDate);
            }
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                SQLMethods.InsertParentData(ID, MotherId);
            }
            foreach (string FatherId in FatherIds)
            {
                SQLMethods.InsertParentData(ID, FatherId);
            }

            resetFields();
        }
    }

    List<Control> controls = new List<Control>();
    private void resetFields()
    {
        GetControlsRecursive(this, controls);
        foreach (Control c in controls)
        {
            if (c.GetType() == typeof(TextBox))
            {
                ((TextBox)c).Clear();
            }
            else if (c.GetType() == typeof(ComboBox))
            {
                ((ComboBox)c).SelectedIndex = 1;
            }
        }
    }
    private static void GetControlsRecursive(Control container, List<Control> controls)
    {
        foreach (Control c in container.Controls)
        {
            controls.Add(c);
            if (c.Controls.Count > 0)
            {
                GetControlsRecursive(c, controls);
            }
        }
    }

    private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
    {
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
    private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
    {
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

    private bool IsValidDate(string input, TextBox box, bool bEmpty = false)
    {
        // Use DateTime.TryParseExact method to validate the format of the input string
#pragma warning disable IDE0059 // Unnecessary assignment of a value | breaks without
        if (!DateTime.TryParseExact(input, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            if (string.IsNullOrEmpty(input) && bEmpty) { return true; }
            box.Focus();
            ErrorMessage($"{input} is not a valid date.");
            return false;
        }
        else return true;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
    }

    private System.Windows.Forms.Timer timer1 = new();

    // Display error message and Set timer to remove error message
    private void ErrorMessage(string message)
    {
        L_ErrorMessageField.Text = message;
        if (timer1.Enabled) timer1.Dispose();
        timer1.Tick += new EventHandler(RemoveErrorMessage);
        timer1.Interval = 5000;
        timer1.Start();
    }

    // Stop timer and remove error message
    private void RemoveErrorMessage(object sender, EventArgs e)
    {
        timer1.Stop();
        L_ErrorMessageField.Text = string.Empty;
    }

    private bool ValidateString(string str, Label lab, TextBox box, bool Empty = false)
    {
        // Check if string is empty or not allowed to be empty
        if (string.IsNullOrEmpty(str) && !Empty)
        {
            box.Focus();
            ErrorMessage("The input at '" + lab.Text + "' cannot be empty");
            return false;
        }
        // Check if string is empty and allowed to be empty
        else if (string.IsNullOrEmpty(str))
        {
            return true;
        }
        // Check if first character is valid
        if (str[0] != 'F' && str[0] != 'M' && str[0] != 'U' && !char.IsNumber(str[0]))
        {
            box.Focus();
            ErrorMessage("The first character of the '" + lab.Text + "' field must be a letter (F, M, or U) or a number.");
            return false;
        }
        // Check if string has a number if first character is valid
        if (!(str[0] != 'F' && str[0] != 'M' && str[0] != 'U'))
        {
            if (str.Length <= 1)
            {
                box.Focus();
                ErrorMessage(lab.Text + " Field must have a number.");
                return false;
            }
        }
        // Check if all characters after the first are numeric
        for (int i = 1; i < str.Length; i++)
        {
            if (!char.IsNumber(str[i]))
            {
                box.Focus();
                ErrorMessage("The characters following the first one in this field must be numeric: " + lab.Text);
                return false;
            }
        }
        return true;
    }

    private void TB_ID_TextChanged(object sender, EventArgs e)
    {
        if (ValidateString(TB_ID.Text, L_ID, TB_ID, true) && !string.IsNullOrEmpty(TB_ID.Text))
        {
            string id = TB_ID.Text;
            resetFields();
            comboBox2.SelectedIndex = SQLMethods.GetSexFromEntity(id);
            textBox3.Text = SQLMethods.GetBirthFromEntity(id);
            textBox3.Text = SQLMethods.GetDeathFromEntity(id);
            comboBox1.SelectedIndex = SQLMethods.GetColorFromEntity(id);
            textBox5.Text = SQLMethods.GetMotherFromEntity(id);
            ArrayList fatherdata = SQLMethods.GetFatherFromEntity(id);
            listBox1.Items.AddRange(fatherdata.ToArray());
        }
    }
}
