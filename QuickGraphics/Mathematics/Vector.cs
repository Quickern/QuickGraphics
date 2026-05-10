using System.Numerics;

namespace QuickGraphics.Mathematics;

public record struct Vector(Number X, Number Y) :
    IUnaryNegationOperators<Vector, Vector>,
    IMultiplyOperators<Vector, Number, Vector>,
    IDivisionOperators<Vector, Number, Vector>
{
    public static implicit operator Vector((Number X, Number Y) tuple) => new Vector(tuple.X, tuple.Y);
    public static implicit operator (Number X, Number Y)(Vector vector) => (vector.X, vector.Y);

    public static Vector operator-(Vector vector) => new Vector(-vector.X, -vector.Y);

    public static Vector operator*(Vector vector, Number scalar) => new Vector(vector.X * scalar, vector.Y * scalar);
    public static Vector operator*(Number scalar, Vector vector) => new Vector(vector.X * scalar, vector.Y * scalar);

    public static Vector operator /(Vector vector, Number scalar) => new Vector(vector.X / scalar, vector.Y / scalar);
}
