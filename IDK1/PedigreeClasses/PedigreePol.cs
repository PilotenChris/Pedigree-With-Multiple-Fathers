using System.Collections;

public class PedigreePol : PedigreeFig {
    private List<(int xc, int yc)> coords;
    public PedigreePol(int x, int y, int measurement, string id, int birth, string mother, ArrayList father, Boolean death, Color color) : base(x, y, measurement, id, birth, mother, father, death, color) {
        setConnectionPX(x);
        setConnectionPY(y);
        setCoords(getConnectionPX(), getConnectionPY());
    }

    public override int getConnectionPX() { return connectionPX; }
    public override int getConnectionPY() { return connectionPY; }
    public override List<(int xc, int yc)> getCoords() {
        return coords;
    }
    public override void setCoords(int connPX, int connPY) {
        coords = new List<(int xc, int yc)> {
            (connPX, connPY - (getHeight() / 2)),
            (connPX + (getHeight() / 2), connPY),
            (connPX, connPY + (getHeight() / 2)),
            (connPX - (getWidth() / 2), connPY)
        };
    }
    public override int getDEX() { return getX(); }
    public override int getDEY() { return getY() + getHeight(); }
    public override int getDSX() { return getX() + getWidth(); }
    public override int getDSY() { return getY(); }
    public override string getId() { return id; }
    public override int getRadius() { return measurement; }
    public override int getBirth() { return birth; }
    public override Boolean getDeath() { return death; }
    public override int getWidth() { return measurement; }
    public override int getHeight() { return measurement; }
    public override int getX() { return x; }
    public override int getY() { return y; }
    public override Color getColor() { return color; }
    public override void setX(int x) {
        setConnectionPX(x);
    }
    public override void setY(int y) {
        setConnectionPY(y);
    }
    public override void setConnectionPX(int x) { connectionPX = x + (getWidth() / 2); }
    public override void setConnectionPY(int y) { connectionPY = y + (getHeight() / 2); }
    public override string ToString() {
        string fathersString = string.Join(",", father.Cast<string>());
        string coordsString = string.Join(",", coords.Cast<string>());
        return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Width: " + measurement + ", Height: " + measurement + ", Coords: " + coordsString + ", Mother: " + mother + ", Father/s: (" + fathersString + "), Death: " + death + ", Color: " + color; 
    }

}
