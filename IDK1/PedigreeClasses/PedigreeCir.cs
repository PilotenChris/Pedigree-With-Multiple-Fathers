using System.Collections;

public class PedigreeCir : PedigreeFig {
    public PedigreeCir(int x, int y, int measurement, string id, int birth, string mother, ArrayList father, Boolean death, Color color) : base(x, y, measurement, id, birth, mother, father, death, color) {
        setConnectionPX(x);
        setConnectionPY(y);
    }

    public override int getConnectionPX() { return connectionPX; }
    public override int getConnectionPY() { return connectionPY; }
    public override List<(int xc, int yc)> getCoords() => throw new NotImplementedException();
    public override void setCoords(int connPX, int connPY) => throw new NotImplementedException();
    public override string getId() { return id; }
    public override int getX() { return x; }
    public override int getY() { return y; }
    public override void setX(int x) {
        setConnectionPX(x);
        this.x = x;
    }
    public override void setY(int y) {
        setConnectionPY(y);
        this.y = y;
    }
    public override int getRadius() { return measurement; }
    public override int getWidth() { return measurement; }
    public override int getHeight() { return measurement; }
    public override void setConnectionPX(int x) { connectionPX = x; }
    public override void setConnectionPY(int y) { connectionPY = y + getRadius(); }
    public override int getBirth() { return birth; }
    public override Boolean getDeath() { return death; }
    public override int getDSX() { return getX() + getRadius(); }
    public override int getDSY() { return getY(); }
    public override int getDEX() { return getX(); }
    public override int getDEY() { return getY() + getRadius(); }
    public override Color getColor() { return color; }
    public override string getMother() { return mother; }
    public override ArrayList getFather() { return father; }
    public override string ToString() {
        string fathersString = string.Join(",", father.Cast<string>());
        return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Radius: " + measurement + ", Mother: " + mother + ", Father/s: (" + fathersString + "), Death: " + death + ", Color: " + color; 
    }
}
