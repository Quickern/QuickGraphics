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

    public delegate void Handler(Canvas canvas, ref ReadOnlySpan<byte> data);

    public static byte Add<T>(Handler handler) where T : unmanaged
    {
        byte index = (byte)Handlers.Count;
        Handlers.Add((Unsafe.SizeOf<T>(), handler));
        return index;
    }

    public static Colour GetNvgColor(Canvas canvas, Color color) => canvas.Nvg.Rgba(color.R, color.G, color.B, color.A);
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

        if (!line.Style.Type.HasFlag(StyleType.Stroke))
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

        canvas.Nvg.Save();

        bool hasStroke = rect.Style.Type.HasFlag(StyleType.Stroke);
        bool hasFill = rect.Style.Type.HasFlag(StyleType.Fill);

        if (hasStroke)
        {
            canvas.Nvg.StrokeColour(Primitives.GetNvgColor(canvas, rect.Style.Paint.Color));
            canvas.Nvg.StrokeWidth(rect.Style.StrokeWidth);
        }
        if (hasFill)
            canvas.Nvg.FillColour(Primitives.GetNvgColor(canvas, rect.Style.Paint.Color));

        canvas.Nvg.BeginPath();

        canvas.Nvg.Rect(rect.TopLeft.X, rect.TopLeft.Y, rect.Size.Width, rect.Size.Height);

        if (hasFill)
            canvas.Nvg.Fill();
        if (hasStroke)
            canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}

internal record struct Circle(Style Style, Point Center, int Radius)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Circle circle = ref MemoryMarshal.AsRef<Circle>(data);

        canvas.Nvg.Save();

        bool hasStroke = circle.Style.Type.HasFlag(StyleType.Stroke);
        bool hasFill = circle.Style.Type.HasFlag(StyleType.Fill);

        if (hasStroke)
        {
            canvas.Nvg.StrokeColour(Primitives.GetNvgColor(canvas, circle.Style.Paint.Color));
            canvas.Nvg.StrokeWidth(circle.Style.StrokeWidth);
        }
        if (hasFill)
            canvas.Nvg.FillColour(Primitives.GetNvgColor(canvas, circle.Style.Paint.Color));


        canvas.Nvg.BeginPath();

        canvas.Nvg.Circle(circle.Center.X, circle.Center.Y, circle.Radius);

        if (hasFill)
            canvas.Nvg.Fill();
        if (hasStroke)
            canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}

internal record struct Ellipse(Style Style, Point Center, int RadiusX, int RadiusY)
{
    public static void Draw(Canvas canvas, ref ReadOnlySpan<byte> data)
    {
        ref readonly Ellipse ellipse = ref MemoryMarshal.AsRef<Ellipse>(data);

        canvas.Nvg.Save();

        bool hasStroke = ellipse.Style.Type.HasFlag(StyleType.Stroke);
        bool hasFill = ellipse.Style.Type.HasFlag(StyleType.Fill);

        if (hasStroke)
        {
            canvas.Nvg.StrokeColour(Primitives.GetNvgColor(canvas, ellipse.Style.Paint.Color));
            canvas.Nvg.StrokeWidth(ellipse.Style.StrokeWidth);
        }
        if (hasFill)
            canvas.Nvg.FillColour(Primitives.GetNvgColor(canvas, ellipse.Style.Paint.Color));


        canvas.Nvg.BeginPath();

        canvas.Nvg.Ellipse(ellipse.Center.X, ellipse.Center.Y, ellipse.RadiusX, ellipse.RadiusY);

        if (hasFill)
            canvas.Nvg.Fill();
        if (hasStroke)
            canvas.Nvg.Stroke();

        canvas.Nvg.Restore();
    }
}
