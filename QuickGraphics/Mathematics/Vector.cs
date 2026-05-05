namespace QuickGraphics.Mathematics;

public record struct Vector(int X, int Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new Vector(tuple.X, tuple.Y);

    public static Vector operator-(Vector vector) => new Vector(-vector.X, -vector.Y);

    public static Vector operator*(Vector vector, int scalar) => new Vector(vector.X * scalar, vector.Y * scalar);
    public static Vector operator*(int scalar, Vector vector) => new Vector(vector.X * scalar, vector.Y * scalar);
}
