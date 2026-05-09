using QuickGraphics.Mathematics;
using ColorTuple3 = (byte Red, byte Green, byte Blue);
using ColorTuple4 = (byte Red, byte Green, byte Blue, byte Alpha);

namespace QuickGraphics;

public record struct Style(StyleType Type, Paint Paint, Number StrokeWidth)
{
    public Style(StyleType type, Paint paint) : this(type, paint, 1) { }

    public static implicit operator Style(Paint paint) => Stroke(paint);
    public static implicit operator Style((Paint Paint, Number Width) value) => Stroke(value.Paint, value.Width);

    public static implicit operator Style(Color color) => Stroke(color);
    public static implicit operator Style(ColorTuple3 color) => Stroke(color);
    public static implicit operator Style(ColorTuple4 color) => Stroke(color);

    public static Style Fill(byte red, byte green, byte blue, byte alpha = 255) => new Style(StyleType.Fill, (red, green, blue, alpha));
    public static Style Fill(Paint paint) => Fill(paint, 1);
    public static Style Fill(Paint paint, Number width) => new Style(StyleType.Fill, paint, width);
    public static Style Stroke(byte red, byte green, byte blue, byte alpha = 255) => new Style(StyleType.Stroke, (red, green, blue, alpha));
    public static Style Stroke(Paint paint) => Stroke(paint, 1);
    public static Style Stroke(Paint paint, Number width) => new Style(StyleType.Stroke, paint, width);
    public static Style OnlyFill(byte red, byte green, byte blue, byte alpha = 255) => new Style(StyleType.OnlyFill, (red, green, blue, alpha));
    public static Style OnlyFill(Paint paint) => new Style(StyleType.OnlyFill, paint);
}

[Flags]
public enum StyleType
{
    None     = 0,
    OnlyFill = 1,
    Stroke   = 1 << 1,
    Fill     = OnlyFill | Stroke
}
