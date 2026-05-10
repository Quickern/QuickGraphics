namespace QuickGraphics.Mathematics;

public record struct Size(Number Width, Number Height)
{
    public static implicit operator Size((Number Width, Number Height) tuple) => new Size(tuple.Width, tuple.Height);
    public static implicit operator (Number Width, Number Height)(Size size) => (size.Width, size.Height);
}
