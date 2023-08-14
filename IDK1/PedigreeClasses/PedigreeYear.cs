using System.Collections;

public class PedigreeYear {
    private int x;
    private int y;
    private int year;
    private Boolean spaceYear;
    private Color color = Color.Gray;
    public PedigreeYear(int x, int y, int year, bool spaceYear) {
        this.x = x;
        this.y = y;
        this.year = year;
        this.spaceYear = spaceYear;
    }

    public int getX() { return x; }
    public int getY() { return y; }
    public void setX(int x) { this.x = x; }
    public void setY(int y) {  this.y = y; }
    public int getYear() { return year; }
    public Boolean getSpaceYear() { return spaceYear; }
    public Color getColor() { return color; }
}
