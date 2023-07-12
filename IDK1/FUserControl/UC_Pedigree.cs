using System.Collections;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Diagnostics;
using System.Windows.Forms;
using PedigreeMF.PedigreeClasses;

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

    private List<Entity> entities = new();
    private async void UpdateEntities() {
        entities.Clear();
        //private async void UpdateEntities() {
        //    entities.Clear();
        //    foreach (var data in await SQLMethods.GetEntityDatabase()) {
        //        entities.Add((data.Item1, data.Item2, SQLMethods.GetDeathFromEntity(data.Item1), data.Item3, (string)SQLMethods.GetMotherFromEntity(data.Item1),
        //            string.Join(",", (string[])SQLMethods.GetFatherFromEntity(data.Item1).ToArray(typeof(string))), data.Item4));
        //    }
        //}
        foreach (var data in await SQLMethods.GetEntityDatabase()) {
            string? tdeath = SQLMethods.GetDeathFromEntity(data.Item1);
            ArrayList tfathers = new ArrayList() { string.Join(",", (string[])SQLMethods.GetFatherFromEntity(data.Item1).ToArray(typeof(string))) };
            string? tmothers = SQLMethods.GetMotherFromEntity(data.Item1);
            Entity entity = new Entity() {
                Id = data.Item1,
                BirthYear = DateTime.Parse(data.Item2).Year,
                Death = tdeath,
                Sex = data.Item3,
                Mother = tmothers,
                Fathers = tfathers,
                FigColor = ChangeColor(data.Item4),
            };

            entities.Add(entity);
            // Debug.WriteLine(entity.ToString());
        }
        Debug.WriteLine(entities);
    }

    private async void UpdatePedigreeFig() {
        foreach (var data in entities) {
            if (data.Sex == SEX1) {
                if (data.Death != null) {
                    PedigreePol pedigreePol = new PedigreePol(1, 1, data.Id, data.BirthYear, data.Mother, data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreePol);
                    //Debug.WriteLine(pedigreePol.ToString());
                }
                else {
                    PedigreePol pedigreePol = new PedigreePol(1, 1, data.Id, data.BirthYear, data.Mother, data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreePol);
                    //Debug.WriteLine(pedigreePol.ToString());
                }
            }
            else if (data.Sex == SEX2) {
                if (data.Death != null) {
                    PedigreeSqu pedigreeSqu = new PedigreeSqu(1, 1, data.Id, data.BirthYear, data.Mother, data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreeSqu);
                    //Debug.WriteLine(pedigreeSqu.ToString());
                }
                else {
                    PedigreeSqu pedigreeSqu = new PedigreeSqu(1, 1, data.Id, data.BirthYear, data.Mother, data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreeSqu);
                    //Debug.WriteLine(pedigreeSqu.ToString());
                }
            }
            else if (data.Sex == SEX3) {
                if (data.Death != null) {
                    PedigreeCir pedigreeCir = new PedigreeCir(1, 1, data.Id, data.BirthYear, data.Mother, data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreeCir);
                    //Debug.WriteLine(pedigreeCir.ToString());
                }
                else {
                    PedigreeCir pedigreeCir = new PedigreeCir(1, 1, data.Id, data.BirthYear, data.Mother, data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreeCir);
                    //Debug.WriteLine(pedigreeCir.ToString());
                }
            }
        }
        Debug.WriteLine(pedigreeTab.Count);
    }

    private static Color ChangeColor(string color) {
        switch (color) {
            case "Light Blue": 
                return Color.LightBlue;
            case "Light Yellow": 
                return Color.LightYellow;
            case "Light Green":
                return Color.LightGreen;
            case "Light Grey":
                return Color.LightGray;
            case "Light Orange":
                return Color.Orange;
            default:
                return Color.LightBlue;
        }
    }
}
