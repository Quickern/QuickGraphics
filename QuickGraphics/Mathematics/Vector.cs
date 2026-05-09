namespace QuickGraphics.Mathematics;

public record struct Vector(Number X, Number Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new Vector(tuple.X, tuple.Y);
    public static implicit operator Vector((float X, float Y) tuple) => new Vector(tuple.X, tuple.Y);
    public static implicit operator Vector((double X, double Y) tuple) => new Vector(tuple.X, tuple.Y);

    public static Vector operator-(Vector vector) => new Vector(-vector.X, -vector.Y);

    public static Vector operator*(Vector vector, int scalar) => new Vector(vector.X * scalar, vector.Y * scalar);
    public static Vector operator*(int scalar, Vector vector) => new Vector(vector.X * scalar, vector.Y * scalar);
}
