using ColorTuple3 = (byte Red, byte Green, byte Blue);
using ColorTuple4 = (byte Red, byte Green, byte Blue, byte Alpha);

namespace QuickGraphics;

public record struct Paint(Color Color)
{
    public static implicit operator Paint(ColorTuple3 color) => new Paint(color);
    public static implicit operator Paint(ColorTuple4 color) => new Paint(color);
    public static implicit operator Paint(Color color) => new Paint(color);
}
