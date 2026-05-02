using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NvgNET;
using NvgNET.Graphics;
using NvgNET.Paths;
using Silk.NET.OpenGL;

namespace QuickGraphics;

internal static class Primitives
{
    public static List<(int DataSize, Handler Draw)> Handlers { get; } = [];

    public static readonly byte Clear = Add<Clear>(QuickGraphics.Clear.Draw);
    public static readonly byte Line = Add<Line>(QuickGraphics.Line.Draw);
    public static readonly byte Rectangle = Add<Rectangle>(QuickGraphics.Rectangle.Draw);
    public static readonly byte Circle = Add<Circle>(QuickGraphics.Circle.Draw);
    public static readonly byte Ellipse = Add<Ellipse>(QuickGraphics.Ellipse.Draw);
    public static readonly byte Bezier = Add<Bezier>(QuickGraphics.Bezier.Draw);

    public delegate void Handler(Canvas canvas, ref ReadOnlySpan<byte> data);

    public static byte Add<T>(Handler handler) where T : unmanaged
    {
        byte index = (byte)Handlers.Count;
        Handlers.Add((Unsafe.SizeOf<T>(), handler));
        return index;
    }

    public static Colour GetNvgColor(Canvas canvas, Color color) => canvas.Nvg.Rgba(color.R, color.G, color.B, color.A);
}

internal ref struct DrawContext : IDisposable
{
    readonly Canvas _canvas;
    readonly Style _style;

    public DrawContext(Canvas canvas, Style style)
    {
        _canvas = canvas;
        _style = style;

        _canvas.Nvg.Save();

        if ((_style.Type & StyleType.Stroke) == StyleType.Stroke)
        {
            _canvas.Nvg.StrokeColour(Primitives.GetNvgColor(_canvas, _style.Paint.Color));
            _canvas.Nvg.StrokeWidth(_style.StrokeWidth);
        }
        if ((_style.Type & StyleType.Fill) == StyleType.Fill)
            _canvas.Nvg.FillColour(Primitives.GetNvgColor(_canvas, _style.Paint.Color));
    }

    public void Dispose()
    {
        if ((_style.Type & StyleType.Fill) == StyleType.Fill)
            _canvas.Nvg.Fill();
        if ((_style.Type & StyleType.Stroke) == StyleType.Stroke)
            _canvas.Nvg.Stroke();

        _canvas.Nvg.Restore();
    }
}

internal record struct Clear(Color Color)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Clear clear = ref MemoryMarshal.AsRef<Clear>(data);

        Colour color = Primitives.GetNvgColor(canvas, clear.Color);

        canvas.Gl.ClearColor(color.R, color.G, color.B, color.A);
        canvas.Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
    }
}

internal record struct Line(Style Style, Point First, Point Second)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Line line = ref MemoryMarshal.AsRef<Line>(data);

        if ((line.Style.Type | StyleType.Stroke) != StyleType.Stroke)
        {
            return;
        }

        canvas.Nvg.Save();

        canvas.Nvg.StrokeColour(Primitives.GetNvgColor(canvas, line.Style.Paint.Color));
        canvas.Nvg.StrokeWidth(line.Style.StrokeWidth);

        canvas.Nvg.BeginPath();
        canvas.Nvg.MoveTo(line.First.X, line.First.Y);
        canvas.Nvg.LineTo(line.Second.X, line.Second.Y);
        canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}

internal record struct Rectangle(Style Style, Point TopLeft, Size Size)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Rectangle rect = ref MemoryMarshal.AsRef<Rectangle>(data);

        using DrawContext context = new DrawContext(canvas, rect.Style);

        canvas.Nvg.BeginPath();
        canvas.Nvg.Rect(rect.TopLeft.X, rect.TopLeft.Y, rect.Size.Width, rect.Size.Height);
    }
}

internal record struct Circle(Style Style, Point Center, int Radius)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Circle circle = ref MemoryMarshal.AsRef<Circle>(data);

        using DrawContext context = new DrawContext(canvas, circle.Style);

        canvas.Nvg.BeginPath();
        canvas.Nvg.Circle(circle.Center.X, circle.Center.Y, circle.Radius);
    }
}

internal record struct Ellipse(Style Style, Point Center, int RadiusX, int RadiusY)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Ellipse ellipse = ref MemoryMarshal.AsRef<Ellipse>(data);

        using DrawContext context = new DrawContext(canvas, ellipse.Style);

        canvas.Nvg.BeginPath();
        canvas.Nvg.Ellipse(ellipse.Center.X, ellipse.Center.Y, ellipse.RadiusX, ellipse.RadiusY);
    }
}

internal record struct Bezier(Style Style, Point P0, Point P1, Point P2, Point P3)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Bezier bezier = ref MemoryMarshal.AsRef<Bezier>(data);

        using DrawContext context = new DrawContext(canvas, bezier.Style);

        canvas.Nvg.BeginPath();
        canvas.Nvg.MoveTo(bezier.P0.X, bezier.P0.Y);
        canvas.Nvg.BezierTo(bezier.P1.X, bezier.P1.Y, bezier.P2.X, bezier.P2.Y, bezier.P3.X, bezier.P3.Y);
    }
}
