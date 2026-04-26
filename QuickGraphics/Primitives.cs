using NvgNET;
using NvgNET.Graphics;
using NvgNET.Paths;

namespace QuickGraphics;

internal abstract class Primitive
{
    public Color Color { get; set; }

    public abstract void Draw(Canvas canvas);
}

internal class Line : Primitive
{
    private Point _first;
    private Point _second;

    public void Initialize(Point first, Point second)
    {
        _first = first;
        _second = second;
    }

    public override void Draw(Canvas canvas)
    {
        canvas.Nvg.Save();

        canvas.Nvg.StrokeColour(new Colour(Color.R, Color.G, Color.B, Color.A));
        
        canvas.Nvg.StrokeWidth(2);
        
        canvas.Nvg.BeginPath();
        canvas.Nvg.MoveTo(_first.X, _first.Y);
        canvas.Nvg.LineTo(_second.X, _second.Y);
        canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}
