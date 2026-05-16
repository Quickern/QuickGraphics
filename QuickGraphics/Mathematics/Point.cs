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

    internal Vector2 AsNumerics() => new Vector2(X, Y);
    internal static Point FromNumerics(Vector2 vector2) => new Point(vector2.X, vector2.Y);
}
