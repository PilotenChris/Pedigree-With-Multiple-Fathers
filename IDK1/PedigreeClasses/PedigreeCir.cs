using System.Collections;

public class PedigreeCir : PedigreeFig {
    private int radius = 50;
    public PedigreeCir(int x, int y, string id, int birth, string mother, ArrayList father, Boolean death, Color color) : base(x, y, id, birth, mother, father, death, color) {
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
    public override int getRadius() { return radius; }
    public override int getWidth() => throw new NotImplementedException();
    public override int getHeight() => throw new NotImplementedException();
    public override void setConnectionPX(int x) { connectionPX = x; }
    public override void setConnectionPY(int y) { connectionPY = y + getRadius(); }
    public override int getBirth() { return birth; }
    public override Boolean getDeath() { return death; }
    public override int getDSX() => throw new NotImplementedException();
    public override int getDSY() => throw new NotImplementedException();
    public override int getDEX() => throw new NotImplementedException();
    public override int getDEY() => throw new NotImplementedException();
    public override Color GetColor() { return color; }
    public override string ToString() {
        string fathersString = string.Join(",", father.Cast<string>());
        return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Radius: " + radius + ", Mother: " + mother + ", Father/s: (" + fathersString + "), Death: " + death + ", Color: " + color; 
    }
}
