using System.Collections;

public class PedigreeSqu : PedigreeFig {
    private int width = 20;
    private int height = 20;
    public PedigreeLiveSqu(int x, int y, string id, int birth, string mother, ArrayList father, Boolean death, Color color) : base(x, y, id, birth, mother, father, death, color) {
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
    public override int getRadius() => throw new NotImplementedException();
    public override int getWidth() { return width; }
    public override int getHeight() { return height; }
    public override void setConnectionPX(int x) { connectionPX = x + (getWidth() / 2); }
    public override void setConnectionPY(int y) { connectionPY = y + getHeight(); }
    public override int getDSX() => throw new NotImplementedException();
    public override int getDSY() => throw new NotImplementedException();
    public override int getDEX() => throw new NotImplementedException();
    public override int getDEY() => throw new NotImplementedException();
    public override string ToString() { return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Width: " + width + ", Height: " + height; }

}
