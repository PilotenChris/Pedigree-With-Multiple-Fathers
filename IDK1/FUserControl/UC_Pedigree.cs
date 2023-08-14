using System.Collections;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PedigreeMF.PedigreeClasses;

namespace IDK1.FUserControl;
public partial class UC_Pedigree : UserControl {
    private readonly string SEX1 = "Unknown";
    private readonly string SEX2 = "Male";
    private readonly string SEX3 = "Female";
    private PictureBox pictureBox1 = new PictureBox();
    private int minYear;
    private int maxYear;
    private readonly int penWidth = 2;
    public UC_Pedigree() {
        InitializeComponent();
        UpdateEntities();
        UpdatePedigreeFig();
        Canvas();
    }
    private List<PedigreeFig> pedigreeTab = new List<PedigreeFig>();
    private List<PedigreeYear> pedigreeYears = new List<PedigreeYear>();

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
        YearPedigreeEntity();
    }

    private void Canvas() {
        pictureBox1.Paint += PictureBox1_Paint;

        pictureBox1.Dock = DockStyle.Fill;
        //pictureBox1.BackColor = Color.FromArgb(0,64,64,64);
        pictureBox1.BackColor = Color.LightGray;

        panel1.Controls.Add(pictureBox1);
    }

    private void PictureBox1_Paint(object sender, PaintEventArgs e) {
        //e.Graphics.Clear(Parent.BackColor);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        foreach (PedigreeFig pedigreeFig in pedigreeTab) {
            if (pedigreeFig is PedigreeCir) {
                PedigreeCir pedigreeCir = (PedigreeCir)pedigreeFig;
                //if ()
                e.Graphics.DrawEllipse(new Pen(pedigreeCir.GetColor(), penWidth), new Rectangle(pedigreeCir.getConnectionPX(), pedigreeCir.getConnectionPY(), pedigreeCir.getRadius(), pedigreeCir.getRadius()));
            } else if (pedigreeFig is PedigreeSqu) {
                PedigreeSqu pedigreeSqu = (PedigreeSqu)pedigreeFig;
                e.Graphics.DrawRectangle(new Pen(pedigreeSqu.GetColor(), penWidth), new Rectangle(pedigreeSqu.getConnectionPX(), pedigreeSqu.getConnectionPY(), pedigreeSqu.getWidth(), pedigreeSqu.getHeight()));
            } else if (pedigreeFig is PedigreePol) {
                //PedigreePol pedigreePol = (PedigreePol)pedigreeFig;
                //e.Graphics.DrawPolygon();
            }
        }
    }

    private void YearPedigreeEntity() {
        List<int> birthYears = pedigreeTab.Select(fig => fig.getBirth()).Distinct().ToList();
        
        // Finds the min and max Year from the entites in the Pedigree
        if (birthYears.Count > 0) {
            minYear = birthYears.Min();
            maxYear = birthYears.Max();
        } else {
            Debug.WriteLine("No birth years found.");
        }
        
        // Makes a list of the years as objects for the Pedigree
        for (int year = minYear; year <= maxYear; year++) {
            pedigreeYears.Add(new PedigreeYear(0, 0, year, false));
            if (year != maxYear) {
                pedigreeYears.Add(new PedigreeYear(0, 0, year + 1, true));
            }
        }
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
