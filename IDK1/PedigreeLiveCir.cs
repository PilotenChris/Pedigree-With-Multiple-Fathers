public class PedigreeLiveCir : PedigreeFig {
    private int radius = 10;
    public PedigreeLiveCir(int x, int y, string id) : base(x, y, id) {
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
    public override int getRadius() { return radius; }
    public override int getWidth() => throw new NotImplementedException();
    public override int getHeight() => throw new NotImplementedException();
    public override void setConnectionPX(int x) { connectionPX = x; }
    public override void setConnectionPY(int y) { connectionPY = y + getRadius(); }
    public override int getDSX() => throw new NotImplementedException();
    public override int getDSY() => throw new NotImplementedException();
    public override int getDEX() => throw new NotImplementedException();
    public override int getDEY() => throw new NotImplementedException();
    public override string ToString() { return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Radius: " + radius; }
}
