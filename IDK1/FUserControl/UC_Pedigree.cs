using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace IDK1.FUserControl;
public partial class UC_Pedigree : UserControl {
    private readonly string SEX1 = "Unknown";
    private readonly string SEX2 = "Male";
    private readonly string SEX3 = "Female";
    public UC_Pedigree() {
        InitializeComponent();
        UpdateEntities();
        UpdatePedigreeFig();
    }
    private List<PedigreeFig> pedigreeTab = new List<PedigreeFig>();

    private ArrayList entities = new();
    private async void UpdateEntities() {
        entities.Clear();
        foreach (var data in await SQLMethods.GetEntityDatabase()) {
            entities.Add((data.Item1, data.Item2, SQLMethods.GetDeathFromEntity(data.Item1), data.Item3, SQLMethods.GetMotherFromEntity(data.Item1),
                string.Join(",", (string[])SQLMethods.GetFatherFromEntity(data.Item1).ToArray(typeof(string))), data.Item4));
        }
    }

    private async void UpdatePedigreeFig() {
        foreach (var data in entities) {
            if (data is Tuple<string ,string, string, string, string, string, string> tup) {
                string id = tup.Item1;
                int birth = DateTime.Parse(tup.Item2).Year;
                string death = tup.Item3;
                string sex = tup.Item4;
                string mother = tup.Item5;
                string father = tup.Item6; // remember to split?
                string color = tup.Item7;
                /*
                if (sex == SEX1) {
                    if (death.Length > 0) {
                        pedigreeTab.Add(new PedigreeLivePol(1, 1, id, birth));
                    }
                    else {

                    }
                }
                else if (sex == SEX2) {
                    if (death.Length > 0) {
                        pedigreeTab.Add(new PedigreeLiveSqu(1, 1, id, birth));
                    }
                    else {

                    }
                }
                else if (sex == SEX3) {
                    if (death.Length > 0) {
                        pedigreeTab.Add(new PedigreeLiveCir(1, 1, id, birth));
                    }
                    else {

                    }
                }
                */

            }

        }
    }
}
