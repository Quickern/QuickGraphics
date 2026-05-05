namespace QuickGraphics.Mathematics;

public record struct Size(int Width, int Height)
{
    public static implicit operator Size((int Width, int Height) tuple) => new Size(tuple.Width, tuple.Height);
}
