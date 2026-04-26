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

        canvas.Nvg.StrokeColour(canvas.Nvg.Rgba(Color.R, Color.G, Color.B, Color.A));
        
        canvas.Nvg.StrokeWidth(2);
        
        canvas.Nvg.BeginPath();
        canvas.Nvg.MoveTo(_first.X, _first.Y);
        canvas.Nvg.LineTo(_second.X, _second.Y);
        canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}

internal class Circle : Primitive
{
    private Point _center;
    private int _radius;

    public void Initialize(Point center, int radius)
    {
        _center = center;
        _radius = radius;
    }

    public override void Draw(Canvas canvas)
    {
        canvas.Nvg.Save();

        canvas.Nvg.StrokeColour(canvas.Nvg.Rgba(Color.R, Color.G, Color.B, Color.A));
        canvas.Nvg.FillColour(canvas.Nvg.Rgba(Color.R, Color.G, Color.B, Color.A));
        
        canvas.Nvg.StrokeWidth(2);
        
        canvas.Nvg.BeginPath();

        canvas.Nvg.Circle(_center.X, _center.Y, _radius);
        canvas.Nvg.Fill();
        canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}

internal class Rectangle : Primitive
{
    private Point _topLeft;
    private Size _size;

    public void Initialize(Point topLeft, Size size)
    {
        _topLeft = topLeft;
        _size = size;
    }

    public override void Draw(Canvas canvas)
    {
        canvas.Nvg.Save();

        canvas.Nvg.StrokeColour(canvas.Nvg.Rgba(Color.R, Color.G, Color.B, Color.A));
        canvas.Nvg.FillColour(canvas.Nvg.Rgba(Color.R, Color.G, Color.B, Color.A));
        
        canvas.Nvg.StrokeWidth(2);
        
        canvas.Nvg.BeginPath();

        canvas.Nvg.Rect(_topLeft.X, _topLeft.Y, _size.Width, _size.Height);
        canvas.Nvg.Fill();
        canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}
