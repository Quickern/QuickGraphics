namespace QuickGraphics.Mathematics;

public record struct Size(Number Width, Number Height)
{
    public static implicit operator Size((int Width, int Height) tuple) => new Size(tuple.Width, tuple.Height);
    public static implicit operator Size((float Width, float Height) tuple) => new Size(tuple.Width, tuple.Height);
    public static implicit operator Size((double Width, double Height) tuple) => new Size(tuple.Width, tuple.Height);
}
