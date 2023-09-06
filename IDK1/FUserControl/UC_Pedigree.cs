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
    private readonly int penWidth = 3;
    private readonly int measurement = 50;
    private readonly int spaceBetweenFig = 20;
    public UC_Pedigree() {
        InitializeComponent();
        UpdateEntities();
        InitiatePedigreeFig();
        Canvas();
    }
    private List<PedigreeFig> pedigreeTab = new List<PedigreeFig>();
    private List<PedigreeYear> pedigreeYears = new List<PedigreeYear>();
    private List<List<PedigreeFig>> pedigreeGrid = new List<List<PedigreeFig>>();

    private List<Entity> entities = new();
    private async void UpdateEntities() {
        entities.Clear();
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

    private async void InitiatePedigreeFig() {
        foreach (var data in entities) {
            if (data.Sex == SEX1) {
                if (data.Death != null) {
                    PedigreePol pedigreePol = new PedigreePol(1, 1, measurement, data.Id, data.BirthYear, data.Mother, data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreePol);
                    //Debug.WriteLine(pedigreePol.ToString());
                }
                else {
                    PedigreePol pedigreePol = new PedigreePol(1, 1, measurement, data.Id, data.BirthYear, data.Mother, data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreePol);
                    //Debug.WriteLine(pedigreePol.ToString());
                }
            }
            else if (data.Sex == SEX2) {
                if (data.Death != null) {
                    PedigreeSqu pedigreeSqu = new PedigreeSqu(1, 1, measurement, data.Id, data.BirthYear, data.Mother, data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreeSqu);
                    //Debug.WriteLine(pedigreeSqu.ToString());
                }
                else {
                    PedigreeSqu pedigreeSqu = new PedigreeSqu(1, 1, measurement, data.Id, data.BirthYear, data.Mother, data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreeSqu);
                    //Debug.WriteLine(pedigreeSqu.ToString());
                }
            }
            else if (data.Sex == SEX3) {
                if (data.Death != null) {
                    PedigreeCir pedigreeCir = new PedigreeCir(1, 1, measurement, data.Id, data.BirthYear, data.Mother, data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreeCir);
                    //Debug.WriteLine(pedigreeCir.ToString());
                }
                else {
                    PedigreeCir pedigreeCir = new PedigreeCir(1, 1, measurement, data.Id, data.BirthYear, data.Mother, data.Fathers, false, data.FigColor);
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
        pictureBox1.BackColor = Color.LightGray;

        panel1.Controls.Add(pictureBox1);
    }

    private void PictureBox1_Paint(object sender, PaintEventArgs e) {
        //e.Graphics.Clear(Parent.BackColor);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Draws out the Years on the canvas
        foreach (PedigreeYear pedigreeYear in pedigreeYears) {
            if (!pedigreeYear.getSpaceYear()) {
                e.Graphics.FillRectangle(new SolidBrush(pedigreeYear.getColor()), new Rectangle(pedigreeYear.getX(), pedigreeYear.getY(), pedigreeYear.getWidth(), pedigreeYear.getHeight() + (pedigreeYear.getHeight()/2)));
                DrawCenteredText(e.Graphics, pedigreeYear.getYear()+"", new Rectangle(pedigreeYear.getX(), pedigreeYear.getY(), pedigreeYear.getWidth(), pedigreeYear.getHeight()));
            } else if (pedigreeYear.getSpaceYear()) {
                e.Graphics.FillRectangle(new SolidBrush(pedigreeYear.getColor()), new Rectangle(pedigreeYear.getX(), pedigreeYear.getY(), pedigreeYear.getWidth(), pedigreeYear.getHeight() + (pedigreeYear.getHeight() / 2)));
            }
        }

        // Draws out the entities on the canvas
        foreach (PedigreeFig pedigreeFig in pedigreeTab) {
            if (pedigreeFig is PedigreeCir) {
                PedigreeCir pedigreeCir = (PedigreeCir)pedigreeFig;
                e.Graphics.DrawEllipse(new Pen(pedigreeCir.getColor(), penWidth), new Rectangle(pedigreeCir.getX(), pedigreeCir.getY(), pedigreeCir.getRadius(), pedigreeCir.getRadius()));
                if (pedigreeCir.getDeath()) {
                    e.Graphics.DrawLine(new Pen(pedigreeCir.getColor(), penWidth), pedigreeCir.getDSX(), pedigreeCir.getDSY(), pedigreeCir.getDEX(), pedigreeCir.getDEY());
                }
                DrawCenteredText(e.Graphics, pedigreeCir.getId(), new Rectangle(pedigreeCir.getX(), pedigreeCir.getY(), pedigreeCir.getRadius(), pedigreeCir.getRadius()));
            } else if (pedigreeFig is PedigreeSqu) {
                PedigreeSqu pedigreeSqu = (PedigreeSqu)pedigreeFig;
                e.Graphics.DrawRectangle(new Pen(pedigreeSqu.getColor(), penWidth), new Rectangle(pedigreeSqu.getX(), pedigreeSqu.getY(), pedigreeSqu.getWidth(), pedigreeSqu.getHeight()));
                if (pedigreeSqu.getDeath()) {
                    e.Graphics.DrawLine(new Pen(pedigreeSqu.getColor(), penWidth), pedigreeSqu.getDSX(), pedigreeSqu.getDSY(), pedigreeSqu.getDEX(), pedigreeSqu.getDEY());
                }
                DrawCenteredText(e.Graphics, pedigreeSqu.getId(), new Rectangle(pedigreeSqu.getX(), pedigreeSqu.getY(), pedigreeSqu.getWidth(), pedigreeSqu.getHeight()));
            } else if (pedigreeFig is PedigreePol) {
                PedigreePol pedigreePol = (PedigreePol)pedigreeFig;
                Point[] points = pedigreePol.getCoords().Select(coord => new Point(coord.xc, coord.yc)).ToArray();
                e.Graphics.DrawPolygon(new Pen(pedigreePol.getColor(), penWidth), points);
                if (pedigreePol.getDeath()) {
                    e.Graphics.DrawLine(new Pen(pedigreePol.getColor(), penWidth), pedigreePol.getDSX(), pedigreePol.getDSY(), pedigreePol.getDEX(), pedigreePol.getDEY());
                }
                DrawCenteredText(e.Graphics, pedigreePol.getId(), GetPolygonBounds(points));
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
        
        // Clear the list of all the years (and spaceYears)
        pedigreeYears.Clear();
        // Makes a list of the years as objects for the Pedigree
        for (int year = minYear; year <= maxYear; year++) {
            pedigreeYears.Add(new PedigreeYear(0, 0, year, measurement, false));
            if (year != maxYear) {
                pedigreeYears.Add(new PedigreeYear(0, 0, year + 1, measurement, true));
            }
        }
        UpdatePedigreeFig();
    }

    private void UpdatePedigreeFig() {
        // Clear the grid of Pedigree figures
        pedigreeGrid.Clear();

        Dictionary<string, List<PedigreeFig>> entityGroups = new Dictionary<string, List<PedigreeFig>>();

        foreach (var pedigreeFig in pedigreeTab) {
            int birthYear = pedigreeFig.getBirth();
            string mother = pedigreeFig.getMother();
            ArrayList fathers = pedigreeFig.getFather();

            // Create a key based on birth year, mother, and fathers
            string key = $"{birthYear}_{mother}_{string.Join(",", fathers)}";

            // Check if the key already exists in the dictionary
            if (!entityGroups.ContainsKey(key)) {
                entityGroups[key] = new List<PedigreeFig>();
            }

            // Add the entity to the corresponding group
            entityGroups[key].Add(pedigreeFig);
        }

        // Initialize the 2D pedigreeGrid List
        for (int year = minYear; year <= maxYear; year++) {
            pedigreeGrid.Add(new List<PedigreeFig>());
        }

        // Add the groups to the pedigreeGrid
        foreach (var group in entityGroups.Values) {
            int birthYear = group[0].getBirth();
            int index = birthYear - minYear;
            pedigreeGrid[index].AddRange(group);
        }

        // Set's the right x and y coords for the PedigreeYear objects
        for (int i = 0; i < pedigreeYears.Count; i++) { 
            int xPosition = 0;
            int yPosition;
            PedigreeYear pedigreeYear = pedigreeYears[i];
            yPosition = i * (measurement + (measurement / 2));
            pedigreeYear.setX(xPosition);
            pedigreeYear.setY(yPosition);
        }

        // Set's the right x and y coords for the PedigreeFig objects
        for (int i = 0; i < pedigreeGrid.Count; i++) {
            int xPosition = measurement + spaceBetweenFig;
            int yPosition = i * (measurement + (measurement*2));

            foreach (PedigreeFig pedigreeFig in pedigreeGrid[i]) {
                pedigreeFig.setX(xPosition);
                pedigreeFig.setY(yPosition);

                //if (pedigreeFig is PedigreeCir )
                xPosition += measurement + spaceBetweenFig;
            }
        }
    }

    private void DrawCenteredText(Graphics g, string text, Rectangle bounds) {
        using (StringFormat stringFormat = new StringFormat()) {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font font = new Font(Font.FontFamily, measurement/4, FontStyle.Bold);

            g.DrawString(text, font, Brushes.Black, bounds, stringFormat);
        }
    }

    private Rectangle GetPolygonBounds(Point[] points) {
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int maxX = int.MinValue;
        int maxY = int.MinValue;

        foreach (Point point in points) {
            minX = Math.Min(minX, point.X);
            minY = Math.Min(minY, point.Y);
            maxX = Math.Max(maxX, point.X);
            maxY = Math.Max(maxY, point.Y);
        }
        return new Rectangle(minX, minY, maxX - minX, maxY - minY);
    }

    private static Color ChangeColor(string color) {
        switch (color) {
            case "Blue": 
                return Color.Blue;
            case "Yellow": 
                return Color.Yellow;
            case "Green":
                return Color.Green;
            case "Grey":
                return Color.Gray;
            case "Orange":
                return Color.Orange;
            default:
                return Color.Blue;
        }
    }
}
