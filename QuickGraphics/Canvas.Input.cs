namespace QuickGraphics;

partial class Canvas
{
    public MouseData Mouse { get; internal set; }
}

public record struct MouseData(Point Position);
