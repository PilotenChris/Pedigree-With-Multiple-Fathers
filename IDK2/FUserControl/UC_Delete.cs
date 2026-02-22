using System.Collections;

namespace IDK1.FUserControl;
public partial class UC_Delete : UserControl {

    private Form1 parentForm;

    public UC_Delete(Form1 form) {
        InitializeComponent();
        this.parentForm = form;
    }

    private void b_DeleteData_Click(object sender, EventArgs e) {
        ErrorMessage("Work in progress");
        if (ValidateString(TB_ID.Text, L_ID, TB_ID)) {
            if (string.IsNullOrEmpty(SQLMethods.GetIDFromEntity(TB_ID.Text))) {
                ErrorMessage("Not Found");
                return;
            }
            ArrayList Parents = SQLMethods.GetParents(TB_ID.Text);
            ArrayList Children = SQLMethods.GetChildren(TB_ID.Text);
            if (Parents.Count > 0 || Children.Count > 0) {
                if (WarnUserAboutAssociatedLinks(TB_ID.Text, (Parents.Count + Children.Count))) {
                    SQLMethods.DeleteAllChildrenLinks(TB_ID.Text);
                    SQLMethods.DeleteEntity(TB_ID.Text);
                }
            }
            //SQLMethods.DeleteEntity(TB_ID.Text);
            resetFields();
        }
        parentForm.updateScreenSelected();
    }

    private bool WarnUserAboutAssociatedLinks(string entityName, int numLinks) {
        string message = $"Are you sure you want to delete all {numLinks} links associated with {entityName}?\nThis will potentially split pedigree trees in half";
        bool result = Utils.ShowDialog(message, "Confirmation");

        // Return the user's choice
        return result;
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

    List<Control> controls = new List<Control>();
    private void resetFields() {
        GetControlsRecursive(this, controls);
        foreach (Control c in controls) {
            if (c.GetType() == typeof(TextBox)) {
                ((TextBox)c).Clear();
            }
            else if (c.GetType() == typeof(ComboBox)) {
                ((ComboBox)c).SelectedIndex = 1;
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

    private bool ValidateString(string str, Label lab, TextBox box, bool Empty = false) {
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
        if (str[0] != 'F' && str[0] != 'M' && str[0] != 'U' && !char.IsNumber(str[0])) {
            box.Focus();
            ErrorMessage("The first character of the '" + lab.Text + "' field must be a letter (F, M, or U) or a number.");
            return false;
        }
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
}
