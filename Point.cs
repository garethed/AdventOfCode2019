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

    public static bool operator ==(Point p1, Point p2)
    {
        return p1.Equals(p2);
    }
    public static bool operator !=(Point p1, Point p2)
    {
        return !p1.Equals(p2);
    }

    public override bool Equals(object obj)
    {
        return  obj is Point && x == ((Point)obj).x && y == ((Point)obj).y;
    }

    public override int GetHashCode() {
        return 486187739 * x + y;
    }

    internal Point Minus(Point other)
    {
        return new Point( x - other.x, y - other.y);
    }

    internal Point RotateRight()
    {
        return new Point(-y, x);
    }

    internal Point RotateLeft()
    {
        return new Point(y, -x);
    }

    internal Point Plus(Point direction)
    {
        return new Point(x + direction.x, y + direction.y);
    }
}