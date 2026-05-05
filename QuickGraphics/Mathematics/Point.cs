namespace QuickGraphics.Mathematics;

public record struct Point(int X, int Y)
{
    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

    public static Point operator+(Point point, Vector vector) => new Point(point.X + vector.X, point.Y + vector.Y);
    public static Point operator-(Point point, Vector vector) => new Point(point.X - vector.X, point.Y - vector.Y);

    public static Vector operator-(Point first, Point second) => new Vector(first.X + second.X, first.Y + second.Y);
}
