using System;

public struct Point {

    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public int x;
    public int y;
    public int d => Math.Abs(x) + Math.Abs(y);

    internal Point Move(int dx, int dy)
    {
        return new Point(x + dx, y + dy);
    }
}