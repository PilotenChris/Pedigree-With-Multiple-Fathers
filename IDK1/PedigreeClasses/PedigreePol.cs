using System.Collections;

public class PedigreePol : PedigreeFig {
    private int width = 20;
    private int height = 20;
    private List<(int xc, int yc)> coords;
    public PedigreePol(int x, int y, string id, int birth, string mother, ArrayList father, Boolean death, Color color) : base(x, y, id, birth, mother, father, death, color) {
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
            (connPX,connPY),(connPX + (getWidth() / 2),connPY + (getHeight() / 2)),(connPX + getWidth(),connPY),(connPX - (getWidth() / 2),connPY - (getHeight() / 2))
        };
    }
    public override int getDEX() => throw new NotImplementedException();
    public override int getDEY() => throw new NotImplementedException();
    public override int getDSX() => throw new NotImplementedException();
    public override int getDSY() => throw new NotImplementedException();
    public override string getId() => throw new NotImplementedException();
    public override int getRadius() => throw new NotImplementedException();
    public override int getWidth() { return width; }
    public override int getHeight() { return height; }
    public override int getX() => throw new NotImplementedException();
    public override int getY() => throw new NotImplementedException();
    public override void setX(int x) {
        setConnectionPX(x);
        this.x = x;
    }
    public override void setY(int y) {
        setConnectionPY(y);
        this.y = y;
    }
    public override void setConnectionPX(int x) { connectionPX = x + (getWidth() / 2); }
    public override void setConnectionPY(int y) { connectionPY = y + (getHeight() / 2); }
    public override string ToString() {
        string fathersString = string.Join(",", father.Cast<string>());
        return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Width: " + width + ", Height: " + height + ", Coords: " + coords + ", Mother: " + mother + ", Father/s: (" + fathersString + "), Death: " + death + ", Color: " + color; 
    }

}
