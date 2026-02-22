using System.Collections;

public class PedigreeYear {
    private int x;
    private int y;
    private int year;
    private int measurement;
    private Boolean spaceYear;
    private Color color = Color.Gray;
    public PedigreeYear(int x, int y, int year, int measurement, bool spaceYear) {
        this.x = x;
        this.y = y;
        this.year = year;
        this.measurement = measurement;
        this.spaceYear = spaceYear;
    }

    public int getX() { return x; }
    public int getY() { return y; }
    public void setX(int x) { this.x = x; }
    public void setY(int y) { this.y = y; }
    public int getYear() { return year; }
    public int getWidth() { return measurement; }
    public int getHeight() { return measurement; }
    public Boolean getSpaceYear() { return spaceYear; }
    public Color getColor() { return color; }
    public override string ToString() {
        return "X: " + x + ", Y: " + y + ", Year: " + year + ", Width: " + measurement + ", Height: " + measurement + ", Space Year: " + spaceYear + ", Color: " + color;
    }
}
