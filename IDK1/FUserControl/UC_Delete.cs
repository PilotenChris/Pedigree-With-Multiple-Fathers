using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDK1.FUserControl;
public partial class UC_Delete : UserControl
{
    public UC_Delete()
    {
        InitializeComponent();
    }

    private void b_DeleteData_Click(object sender, EventArgs e)
    {
        if (ValidateString(TB_ID.Text, L_ID, TB_ID))
        {

        }
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
}
