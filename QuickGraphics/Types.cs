namespace QuickGraphics;

public record struct Vector(int X, int Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new Vector(tuple.X, tuple.Y);

    public static Vector operator-(Vector vector) => new Vector(-vector.X, -vector.Y);

    public static Vector operator*(Vector vector, int scalar) => new Vector(vector.X * scalar, vector.Y * scalar);
    public static Vector operator*(int scalar, Vector vector) => new Vector(vector.X * scalar, vector.Y * scalar);
}

public record struct Point(int X, int Y)
{
    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

    public static Point operator+(Point point, Vector vector) => new Point(point.X + vector.X, point.Y + vector.Y);
    public static Point operator-(Point point, Vector vector) => new Point(point.X - vector.X, point.Y - vector.Y);

    public static Vector operator-(Point first, Point second) => new Vector(first.X + second.X, first.Y + second.Y);
}

public record struct Size(int Width, int Height)
{
    public static implicit operator Size((int Width, int Height) tuple) => new Size(tuple.Width, tuple.Height);
}

public record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public Color(uint value) : this((byte)((value & 0xff000000) >> 0x18), (byte)((value & 0x00ff0000) >> 0x10), (byte)((value & 0x0000ff00) >> 0x8), (byte)(value & 0x000000ff)) { }

    public static implicit operator Color((byte R, byte G, byte B, byte A) tuple) => new Color(tuple.R, tuple.G, tuple.B, tuple.A);
}

public record struct Paint(Color Color)
{
    public static implicit operator Paint(Color color) => new Paint(color);
}

public record struct Style(StyleType Type, Paint Paint, int StrokeWidth = 1)
{
    public static implicit operator Style(Paint paint) => Stroke(paint);
    public static implicit operator Style((Paint Paint, int Width) value) => Stroke(value.Paint, value.Width);
    public static implicit operator Style(Color color) => Stroke(color);
    public static implicit operator Style((Color Color, int Width) value) => Stroke(value.Color, value.Width);

    public static Style Fill(Paint paint) => new Style(StyleType.Fill, paint);
    public static Style Stroke(Paint paint, int width = 1) => new Style(StyleType.Stroke, paint, width);
}

[Flags]
public enum StyleType
{
    None          = 0,
    Fill          = 1,
    Stroke        = 1 << 1,
    FillAndStroke = Fill | Stroke
}
