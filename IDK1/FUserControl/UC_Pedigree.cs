using System.Collections;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
    private static readonly int penWidth = 3;
    private readonly int measurement = 75;
    private readonly int spaceBetweenFig = 20;
    private int canvasHeight;
    private int canvasWidth;
    private Color bgColor = Color.LightGray;
    private Color pngBgColor = Color.White;
    public UC_Pedigree() {
        InitializeComponent();
        UpdateEntities();
        InitiatePedigreeFig();
        Canvas();
    }
    
    // Initialize lists to store pedigree data and entities
    private List<PedigreeFig> pedigreeTab = new List<PedigreeFig>();
    private List<PedigreeYear> pedigreeYears = new List<PedigreeYear>();
    private List<List<PedigreeFig>> pedigreeGrid = new List<List<PedigreeFig>>();

    // Initialize a list to store entity data
    private List<Entity> entities = new List<Entity>();

    // This method updates the list of entities asynchronously
    private async void UpdateEntities() {
        // Clear the list of entities to start with a clean slate
        entities.Clear();

        // Retrieve entity data from a SQLite database asynchronously
        foreach (var data in await SQLMethods.GetEntityDatabase()) {
            // Get additional information related to each entity
            string? tdeath = SQLMethods.GetDeathFromEntity(data.Item1);
            ArrayList tfathers = new ArrayList() { string.Join(",", (string[])SQLMethods.GetFatherFromEntity(data.Item1).ToArray(typeof(string))) };
            string? tmothers = SQLMethods.GetMotherFromEntity(data.Item1);

            // Create an Entity object and populate it with data
            Entity entity = new Entity() {
                Id = data.Item1,
                BirthYear = DateTime.Parse(data.Item2).Year,
                Death = tdeath,
                Sex = data.Item3,
                Mother = tmothers,
                Fathers = tfathers,
                FigColor = ChangeColor(data.Item4),
            };

            // Add the entity to the list of entities
            entities.Add(entity);
        }
    }

    private void InitiatePedigreeFig() {
        foreach (var data in entities) {
            if (data.Sex == SEX1) {
                if (data.Death != null) {
                    PedigreePol pedigreePol = new PedigreePol(1, 1, measurement, data.Id, data.BirthYear, data.Mother, (ArrayList)data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreePol);
                }
                else {
                    PedigreePol pedigreePol = new PedigreePol(1, 1, measurement, data.Id, data.BirthYear, data.Mother, (ArrayList)data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreePol);
                }
            }
            else if (data.Sex == SEX2) {
                if (data.Death != null) {
                    PedigreeSqu pedigreeSqu = new PedigreeSqu(1, 1, measurement, data.Id, data.BirthYear, data.Mother, (ArrayList)data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreeSqu);
                }
                else {
                    PedigreeSqu pedigreeSqu = new PedigreeSqu(1, 1, measurement, data.Id, data.BirthYear, data.Mother, (ArrayList)data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreeSqu);
                }
            }
            else if (data.Sex == SEX3) {
                if (data.Death != null) {
                    PedigreeCir pedigreeCir = new PedigreeCir(1, 1, measurement, data.Id, data.BirthYear, data.Mother, (ArrayList)data.Fathers, true, data.FigColor);
                    pedigreeTab.Add(pedigreeCir);
                }
                else {
                    PedigreeCir pedigreeCir = new PedigreeCir(1, 1, measurement, data.Id, data.BirthYear, data.Mother, (ArrayList)data.Fathers, false, data.FigColor);
                    pedigreeTab.Add(pedigreeCir);
                }
            }
        }
        YearPedigreeEntity();
    }

    // Set up the canvas for drawing pedigree information
    private void Canvas() {
        // Attach a Paint event handler to the picture box
        pictureBox1.Paint += PictureBox1_Paint;

        // Set the picture box properties
        pictureBox1.Dock = DockStyle.Fill;
        pictureBox1.BackColor = bgColor;

        // Enable auto-scrolling for the panel and set its size
        panel1.AutoScroll = true;
        panel1.AutoScrollMinSize = new Size(canvasWidth, canvasHeight);

        // Add the picture box to the panel
        panel1.Controls.Add(pictureBox1);
    }

    private void PictureBox1_Paint(object sender, PaintEventArgs e) {
        // Set graphics smoothing mode for better rendering
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Draws out the Years on the canvas
        foreach (PedigreeYear pedigreeYear in pedigreeYears) {
            if (!pedigreeYear.getSpaceYear()) {
                e.Graphics.FillRectangle(new SolidBrush(pedigreeYear.getColor()), new Rectangle(pedigreeYear.getX(), pedigreeYear.getY(), pedigreeYear.getWidth(), pedigreeYear.getHeight() + (pedigreeYear.getHeight() / 2)));
                DrawCenteredText(e.Graphics, pedigreeYear.getYear() + "", new Rectangle(pedigreeYear.getX(), pedigreeYear.getY(), pedigreeYear.getWidth(), pedigreeYear.getHeight()));
            }
            else if (pedigreeYear.getSpaceYear()) {
                e.Graphics.FillRectangle(new SolidBrush(pedigreeYear.getColor()), new Rectangle(pedigreeYear.getX(), pedigreeYear.getY(), pedigreeYear.getWidth(), pedigreeYear.getHeight() + (pedigreeYear.getHeight() / 2)));
            }
        }

        // Initialize data structures for drawing connections
        Dictionary<string, int> childMiddlePoints = new Dictionary<string, int>();
        Dictionary<string, int> parentGroupAverages = new Dictionary<string, int>();

        // Loop through pedigree figures to draw connections and calculate averages
        foreach (PedigreeFig pedigreeFig in pedigreeTab) {
            // Construct a unique key for the parent group based on birth, mother, and fathers
            string parentGroupKey = $"{pedigreeFig.getBirth()}_{pedigreeFig.getMother()}_{string.Join(",", pedigreeFig.getFather().Cast<string>())}";
            
            // Calculate the middle point for this parent group if not already calculated
            if (!childMiddlePoints.ContainsKey(parentGroupKey)) {
                // Calculate the middle point by averaging ConnectionPX values of children in the group
                int middleX = (int)pedigreeTab.Where(child => $"{child.getBirth()}_{child.getMother()}_{string.Join(",", child.getFather().Cast<string>())}" == parentGroupKey)
                    .Select(child => child.getConnectionPX()).Average();
                childMiddlePoints[parentGroupKey] = middleX;
            }

            // Check if the current pedigree figure has parents (mother or more than one father)
            if (pedigreeFig.getMother() != null || pedigreeFig.getFather().Count > 1) {
                
                // Draw lines from children to their group's middle point
                // Check if the pedigree figure is a polygonal shape
                if (pedigreeFig is PedigreePol) {
                    e.Graphics.DrawLine(new Pen(Color.Black, penWidth), pedigreeFig.getConnectionPX(), pedigreeFig.getConnectionPY() - (measurement / 2), childMiddlePoints[parentGroupKey], pedigreeFig.getConnectionPY() - measurement);
                }
                else {
                    e.Graphics.DrawLine(new Pen(Color.Black, penWidth), pedigreeFig.getConnectionPX(), pedigreeFig.getConnectionPY() - measurement, childMiddlePoints[parentGroupKey], pedigreeFig.getConnectionPY() - ((measurement * 2) - (measurement / 2)));
                }

                // Initialize variables for parent positions and maximum Y coordinate
                int motherX = 0;
                int motherY = 0;
                int fatherY = 0;
                int maxY = 0;

                // Initialize variables for calculating the average X coordinate of parents
                int averageX = 0;

                // Lists to store X and Y coordinates of fathers and the list of fathers
                List<int> fatherAXId = new List<int>();
                List<int> fatherAYId = new List<int>();
                ArrayList fathers = new ArrayList();
                
                // Check if the parent group's average X coordinate is already calculated
                if (parentGroupAverages.ContainsKey(parentGroupKey)) {
                    // Retrieve the existing average X coordinate
                    averageX = parentGroupAverages[parentGroupKey];
                    // Update the average X coordinate in the dictionary
                    parentGroupAverages[parentGroupKey] = averageX;
                } else {
                    // Calculate the mother's X and Y coordinates if the mother exists
                    if (pedigreeFig.getMother() != null) {
                        PedigreeFig motherFig = pedigreeTab.FirstOrDefault(child => child.getId() == pedigreeFig.getMother());
                        motherX = motherFig.getConnectionPX();
                        motherY = motherFig.getConnectionPY();
                    }


                    // Calculate positions of fathers if there are multiple fathers
                    if (pedigreeFig.getFather().Count > 0) {
                        // Extract and process the father information
                        foreach (object fatherObject in pedigreeFig.getFather()) {
                            string fatherString = fatherObject.ToString();
                            if (!string.IsNullOrEmpty(fatherString)) {
                                string[] fatherArray = fatherString.Split(',');
                                for (int i = 0; i < fatherArray.Length; i++) {
                                    fathers.Add(fatherArray[i]);
                                }
                            }
                        }

                        // Calculate X and Y coordinates for each father
                        foreach (string fatherId in fathers) {
                            PedigreeFig fatherFig = pedigreeTab.FirstOrDefault(child => child.getId() == fatherId);
                            if (fatherFig != null) {
                                fatherAXId.Add(fatherFig.getConnectionPX());
                                fatherAYId.Add(fatherFig.getConnectionPY());
                            }
                        }

                        // Find the maximum Y coordinate among mothers and fathers
                        if (fatherAYId.Count > 0) {
                            fatherY = fatherAYId.Max();
                        }
                        maxY = Math.Max(motherY, fatherY);
                    }
                    // Calculate the Y coordinate for the connection between child and parents
                    int parentY;
                    if (pedigreeFig is PedigreePol) {
                        parentY = pedigreeFig.getConnectionPY() - (measurement);
                    }
                    else {
                        parentY = pedigreeFig.getConnectionPY() - (measurement + (measurement / 2));
                    }

                    // Calculate the average X coordinate for the parent group
                    if (pedigreeFig.getMother() != null && pedigreeFig.getFather().Count > 0) {
                        int totalX = motherX;
                        foreach (int fatherX in fatherAXId) {
                            totalX += fatherX;
                        }
                        averageX = totalX / (fatherAXId.Count + 1);

                        // Ensure that the average X coordinate is not the same as the mother's X coordinate
                        if (averageX == motherX) {
                            averageX += spaceBetweenFig;
                        }

                        // Ensure that the averageX is not too close to existing values in parentGroupAverages
                        while (IsCloseToExistingValue(parentGroupAverages, averageX, 10)) {
                            averageX += 15;
                        }

                        // Ensure that the calculated average X coordinate is unique among parent groups
                        while (parentGroupAverages.ContainsValue(averageX)) {
                            averageX += spaceBetweenFig;
                        }

                        // Update the parent group's average X coordinate in the dictionary
                        parentGroupAverages[parentGroupKey] = averageX;

                        // Draw lines connecting the child to its parents
                        if (pedigreeFig.getMother() != null) {
                            // Draw a line from the mother to the average X coordinate
                            e.Graphics.DrawLine(new Pen(Color.Black, penWidth), motherX, motherY, averageX, maxY + measurement);
                        }
                        if (pedigreeFig.getFather().Count > 0) {
                            // Draw lines from fathers to the average X coordinate
                            foreach (string fatherId in fathers) {
                                PedigreeFig fatherFig = pedigreeTab.FirstOrDefault(child => child.getId() == fatherId);
                                if (fatherFig != null) {
                                    int fatherFX = fatherFig.getConnectionPX();
                                    int fatherFY = fatherFig.getConnectionPY();

                                    // Check if there are multiple fathers, and draw dotted lines if necessary
                                    if (fathers.Count > 1) {
                                        DrawDottedLine(e.Graphics, new Pen(Color.Black, penWidth), fatherFX, fatherFY, averageX, maxY + measurement);
                                    } else {
                                        e.Graphics.DrawLine(new Pen(Color.Black, penWidth), fatherFX, fatherFY, averageX, maxY + measurement);
                                    }
                                }
                            }
                        }

                        e.Graphics.DrawLine(new Pen(Color.Black, penWidth), averageX, maxY + measurement, childMiddlePoints[parentGroupKey], parentY);
                        
                    }
                }
            }
        }

        // Draws out the entities on the canvas with their ID in the center
        foreach (PedigreeFig pedigreeFig in pedigreeTab) {
            if (pedigreeFig is PedigreeCir) {
                // Draws the pedigreeCir entity and center text
                PedigreeCir pedigreeCir = (PedigreeCir)pedigreeFig;
                e.Graphics.DrawEllipse(new Pen(pedigreeCir.getColor(), penWidth), new Rectangle(pedigreeCir.getX(), pedigreeCir.getY(), pedigreeCir.getRadius(), pedigreeCir.getRadius()));
                if (pedigreeCir.getDeath()) {
                    e.Graphics.DrawLine(new Pen(pedigreeCir.getColor(), penWidth), pedigreeCir.getDSX(), pedigreeCir.getDSY(), pedigreeCir.getDEX(), pedigreeCir.getDEY());
                }
                DrawCenteredText(e.Graphics, pedigreeCir.getId(), new Rectangle(pedigreeCir.getX(), pedigreeCir.getY(), pedigreeCir.getRadius(), pedigreeCir.getRadius()));
            }
            else if (pedigreeFig is PedigreeSqu) {
                // Draws the pedigreeSqu entity and center text
                PedigreeSqu pedigreeSqu = (PedigreeSqu)pedigreeFig;
                e.Graphics.DrawRectangle(new Pen(pedigreeSqu.getColor(), penWidth), new Rectangle(pedigreeSqu.getX(), pedigreeSqu.getY(), pedigreeSqu.getWidth(), pedigreeSqu.getHeight()));
                if (pedigreeSqu.getDeath()) {
                    e.Graphics.DrawLine(new Pen(pedigreeSqu.getColor(), penWidth), pedigreeSqu.getDSX(), pedigreeSqu.getDSY(), pedigreeSqu.getDEX(), pedigreeSqu.getDEY());
                }
                DrawCenteredText(e.Graphics, pedigreeSqu.getId(), new Rectangle(pedigreeSqu.getX(), pedigreeSqu.getY(), pedigreeSqu.getWidth(), pedigreeSqu.getHeight()));
            }
            else if (pedigreeFig is PedigreePol) {
                // Draws the pedigreePol entity and center text
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
        }
        else {
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

    private void DrawDottedLine(Graphics g, Pen pen, int x1, int y1, int x2, int y2) {
        // Draws dotted lines for fathers when a child group have more than 1 parent
        float[] dashValues = { 5, 5 };
        pen.DashPattern = dashValues;
        g.DrawLine(pen, x1, y1, x2, y2);
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
            string key = $"{birthYear}_{mother}_{string.Join(",", fathers.Cast<string>())}";

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
            int yPosition = i * (measurement + ((measurement * 2)-1));

            foreach (PedigreeFig pedigreeFig in pedigreeGrid[i]) {
                pedigreeFig.setX(xPosition);
                pedigreeFig.setY(yPosition);

                //if (pedigreeFig is PedigreeCir )
                xPosition += measurement + spaceBetweenFig;
            }
        }
        SetCanvasSize();
    }

    private void DrawCenteredText(Graphics g, string text, Rectangle bounds) {
        // Draws the ID for the PedigreeFig objects in the center of the shape
        // drawn on the Canvas
        using (StringFormat stringFormat = new StringFormat()) {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font font = new Font(Font.FontFamily, measurement / 4, FontStyle.Bold);

            g.DrawString(text, font, Brushes.Black, bounds, stringFormat);
        }
    }

    // Helper function to check if the value is too close to existing values in the dictionary
    private bool IsCloseToExistingValue(Dictionary<string, int> dictionary, int value, int minDistance) {
        foreach (int existingValue in dictionary.Values) {
            if (Math.Abs(existingValue - value) < minDistance) {
                return true;
            }
        }
        return false;
    }

    private void SetCanvasSize() {
        // Sets the Canvas size by the max y value from pedigreeYear objects,
        // and the x value by the max x value from pedigreeFig objects
        int maxYV = int.MinValue;
        int maxXV = int.MinValue;

        foreach (PedigreeYear pedigreeYear in pedigreeYears) {
            int yearHeight = pedigreeYear.getY() + pedigreeYear.getHeight() + (measurement / 2);
            if (yearHeight > maxYV) {
                maxYV = yearHeight;
            }
        }

        foreach (PedigreeFig pedigreeFig in pedigreeTab) {
            int figWidth = pedigreeFig.getX() + pedigreeFig.getWidth() + (measurement / 2);
            if (figWidth > maxXV) {
                maxXV = figWidth;
            }
        }

        canvasWidth = maxXV;
        canvasHeight = maxYV;
    }

    public void SaveCanvasAsPNG(string filePath) {
        // Saves the Canvas as a PNG to user specified file path
        // Creates a Bitmap of the size of the canvas
        using (Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height)) { 
            // Create a Graphics object from Bitmap
            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Sets the background to white and draw the canvas content
                pictureBox1.BackColor = pngBgColor;
                pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            }
            // Save the Bitmap as a PNG file
            bitmap.Save(filePath, ImageFormat.Png);
        }
        // Sets the background color back to normal
        pictureBox1.BackColor = bgColor;
    }

    private Rectangle GetPolygonBounds(Point[] points) {
        // Makes a rectangle for drawing text in the middle of a polygone
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
        // Get color by name from Entity
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
