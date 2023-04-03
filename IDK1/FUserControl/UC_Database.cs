using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDK1;

namespace PedigreeMF.FUserControl;
public partial class UC_Database : UserControl {
    public UC_Database() {
        InitializeComponent();
        GenerateTable();
    }

    private async void GenerateTable() {
        // Create a new DataGridView control and add it to the user control
        var dataGridView = new DataGridView();
        dataGridView.ReadOnly= true;
        dataGridView.Dock = DockStyle.Fill;
        this.Controls.Add(dataGridView);

        // Add columns to the DataGridView
        dataGridView.Columns.Add("Column1", "ID");
        dataGridView.Columns.Add("Column2", "Birth");
        dataGridView.Columns.Add("column3", "Death");
        dataGridView.Columns.Add("Column4", "Sex");
        dataGridView.Columns.Add("Column5", "Mother");
        dataGridView.Columns.Add("Column6", "Father/s");
        dataGridView.Columns.Add("Column7", "Color");

        // Add rows to the DataGridView based on the data in the list
        foreach (var data in await SQLMethods.GetEntityDatabase()) {
            dataGridView.Rows.Add(data.Item1, data.Item2, SQLMethods.GetDeathFromEntity(data.Item1), data.Item3, SQLMethods.GetMotherFromEntity(data.Item1), 
                string.Join(",", (string[])SQLMethods.GetFatherFromEntity(data.Item1).ToArray(typeof(string))), data.Item4);
        }
    }

}
