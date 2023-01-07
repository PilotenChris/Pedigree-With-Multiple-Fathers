using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace IDK1.FUserControl;
public partial class UC_Insert : UserControl
{
    public UC_Insert()
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
        comboBox1.DrawItem += comboBox2_DrawItem;

        comboBox2.Items.AddRange(sexData.ToArray());
        comboBox2.SelectedIndex = 0;
        comboBox2.DrawMode = DrawMode.OwnerDrawFixed;
        comboBox2.DrawItem += comboBox2_DrawItem;

    }

    // Event handler for Father Add button
    private void button1_Click(object sender, EventArgs e)
    {
        checkIfTheMFFieldIsValid(textBox6, "m", "f", label6);
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
        if (checkIfTheMFFieldIsValid(textBox5, "f", "m", label5) && //F
            checkIfTheMFFieldIsValid(textBox6, "m", "f", label6) && //M
            IsValidDate(textBox2.Text, textBox2) && // Birth date
            IsValidDate(textBox3.Text, textBox3) && // Death date
            IsValidID(TB_ID)) 
        {
            ErrorMessage("Success");
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

    private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
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

    private bool IsValidID(TextBox box)
    {
        // Check if the first character is 'F', 'M', or a digit
        var str = box.Text;
        if (!(char.IsLetter(str[0]) && (str[0] == 'F' || str[0] == 'M')) && !char.IsDigit(str[0]))
        {
            box.Focus();
            ErrorMessage("The first character can only be F/M or a number");
            return false;
        }

        // Check if the rest of the characters are digits
        for (int i = 1; i < str.Length; i++)
        {
            if (!char.IsDigit(str[i]))
            {
                ErrorMessage("The following characters after F/M/[NUMBER] can only be numbers");
                return false;              
            }
        }
        return true;
    }

    private bool IsValidDate(string input, TextBox box)
    {
        // Use DateTime.TryParseExact method to validate the format of the input string
        if(!DateTime.TryParseExact(input, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            box.Focus();
            ErrorMessage($"{input} is not a valid date");
            return false;
        }
        else return true; 
    }

    private System.Windows.Forms.Timer timer1 = new();

    // Display error message and Set timer to remove error message
    private void ErrorMessage(string message)
    {
        L_ErrorMessageField.Text = message;
        
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

    private bool checkIfTheMFFieldIsValid(TextBox box, string valid, string invalid, Label label)
    {
        // Do nothing if text box is empty
        if (box.Text == string.Empty)
        {
            return true;
        }
        // Add uppercase valid string + box.text to list box and clear text box if text consists only of digits
        else if (box.Text.ToString().All(char.IsDigit))
        {
            if (box == textBox6)
            {
                listBox1.Items.Add(valid.ToUpper() + box.Text);
                box.Text = string.Empty;
                listbox1Update();
            }
            return true;
        }
        // Add text to list box and clear text box if first character is uppercase valid string
        else if (box.Text[0].ToString().ToUpper() == valid.ToUpper())
        {
            if (box == textBox6 && box.Text.Substring(1).All(char.IsDigit))
            {
                listBox1.Items.Add(box.Text);
                box.Text = string.Empty;
                listbox1Update();
            }
            return true;
        }
        // Set focus on text box and call ErrorMessage with invalid string message if first character is uppercase invalid string
        else if (box.Text[0].ToString().ToUpper() == invalid.ToUpper())
        {
            box.Focus();
            ErrorMessage(invalid + " is not a valid character for the field: " + label);
            return false;
        }
        // Set focus on text box and call ErrorMessage with characters after M/F message if text does not consist of only digits
        else if (!box.Text.All(char.IsDigit))
        {
            box.Focus();
            ErrorMessage("The characters after M/F may only be numbers");
            return false;
        }
        // Set focus on text box and call ErrorMessage with general invalid field message if none of the above conditions are met
        else
        {
            box.Focus();
            ErrorMessage("This field is not valid.");
            return false;
        }
    }

}
