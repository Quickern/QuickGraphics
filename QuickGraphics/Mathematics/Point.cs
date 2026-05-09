namespace QuickGraphics.Mathematics;

public record struct Point(Number X, Number Y)
{
    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);
    public static implicit operator Point((float X, float Y) tuple) => new Point(tuple.X, tuple.Y);
    public static implicit operator Point((double X, double Y) tuple) => new Point(tuple.X, tuple.Y);

    public static implicit operator (int X, int Y)(Point point) => (point.X, point.Y);
    public static implicit operator (float X, float Y)(Point point) => (point.X, point.Y);
    public static implicit operator (double X, double Y)(Point point) => (point.X, point.Y);

    public static Point operator+(Point point, Vector vector) => new Point(point.X + vector.X, point.Y + vector.Y);
    public static Point operator-(Point point, Vector vector) => new Point(point.X - vector.X, point.Y - vector.Y);

    public static Vector operator-(Point first, Point second) => new Vector(first.X + second.X, first.Y + second.Y);
}
