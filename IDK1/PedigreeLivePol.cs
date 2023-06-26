﻿public class PedigreeLivePol : PedigreeFig {
    private int width = 20;
    private int height = 20;
    private List<(int xc, int yc)> coords;
    public PedigreeLivePol(int x, int y, string id, int brith) : base(x, y, id, brith) {
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
    public override void setConnectionPX(int x) { connectionPX = x + (getWidth() / 2); }
    public override void setConnectionPY(int y) { connectionPY = y + (getHeight() / 2); }
    public override string ToString() { return "X: " + x + ", Y: " + y + ", Id: " + id + ", CPX: " + connectionPX + ", CPY: " + connectionPY + ", Width: " + width + ", Height: " + height + ", Coords: " + coords; }

}
