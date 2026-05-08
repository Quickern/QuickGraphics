using QuickGraphics.Mathematics;

namespace QuickGraphics.Drawing;

partial class Canvas
{
    private Point _mousePosition;

    public MouseData Mouse { get; private set; }

    internal void PrepareMouse(Point mousePosition)
    {
        _mousePosition = mousePosition;
    }
}

public record struct MouseData(Point Position);
