using System.Numerics;

namespace QuickGraphics.Mathematics;

public record struct Point(Number X, Number Y) :
    IAdditionOperators<Point, Vector, Point>,
    ISubtractionOperators<Point, Vector, Point>,
    ISubtractionOperators<Point, Point, Vector>
{
    public static implicit operator Point((Number X, Number Y) tuple) => new Point(tuple.X, tuple.Y);
    public static implicit operator (Number X, Number Y)(Point point) => (point.X, point.Y);

    public static Point operator+(Point point, Vector vector) => new Point(point.X + vector.X, point.Y + vector.Y);
    public static Point operator-(Point point, Vector vector) => new Point(point.X - vector.X, point.Y - vector.Y);

    public static Vector operator-(Point first, Point second) => new Vector(first.X + second.X, first.Y + second.Y);
}
