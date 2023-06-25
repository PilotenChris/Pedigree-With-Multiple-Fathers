public abstract class PedigreeFig {
    protected int x;
    protected int y;
    protected string id;
    protected int connectionPX;
    protected int connectionPY;
    public PedigreeFig(int x, int y, string id) {
        this.x = x;
        this.y = y;
        this.id = id;
    }
    public abstract int getX();
    public abstract int getY();
    public abstract string getId();
    public abstract int getConnectionPX();
    public abstract int getConnectionPY();
    public abstract void setConnectionPX(int px);
    public abstract void setConnectionPY(int py);
    public abstract List<(int xc, int yc)> getCoords();
    public abstract void setCoords(int connPX, int connPY);
    public abstract int getRadius();
    public abstract int getWidth();
    public abstract int getHeight();
    public abstract int getDSX();
    public abstract int getDSY();
    public abstract int getDEX();
    public abstract int getDEY();
    public override abstract string ToString();
}
