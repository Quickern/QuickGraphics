namespace QuickGraphics;

public record struct Vector(int X, int Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new Vector(tuple.X, tuple.Y);
}

public record struct Point(int X, int Y)
{
    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);
}

public record struct Size(int Width, int Height)
{
    public static implicit operator Size((int Width, int Height) tuple) => new Size(tuple.Width, tuple.Height);
}

public record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public static implicit operator Color((byte R, byte G, byte B, byte A) tuple) => new Color(tuple.R, tuple.G, tuple.B, tuple.A);
}
