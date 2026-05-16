using QuickGraphics.Drawing;
using QuickGraphics.Drawing.Input;
using QuickGraphics.Mathematics;

namespace QuickGraphics;

public static class QgCanvas
{
    internal static Func<Size, Canvas> CanvasResolver { get; set; } = size => new WindowCanvas(size);

    private static Canvas? s_canvas;

    public static bool IsClosed => Canvas.IsClosed;
    public static bool IsNotClosed => !IsClosed;

    public static Canvas Canvas => s_canvas ?? throw new NoCanvasException($"Call `await {ForCanvas}(width, height);` before any draw.");

    public static FrameAwaitable ForFrame => Canvas.ForFrame;
    public static Task ForExit => Canvas.ForExit;

    public static TimeSpan Time => Canvas.Time;
    public static Number TimeSeconds => Canvas.TimeSeconds;
    public static Number FrameTime => Canvas.FrameTime;

    public static CanvasRunAwaitable ForCanvas(Number width, Number height)
    {
        s_canvas = CanvasResolver(new Size(width, height));

        return new CanvasRunAwaitable(s_canvas);
    }

    public static IMouse Mouse => Canvas.Mouse;
    public static IKeyboard Keyboard => Canvas.Keyboard;

    public static void Clear() => Canvas.Clear();
    public static void Clear(Color color) => Canvas.Clear(color);

    public static void Line(Style style, Point first, Point second) => Canvas.Line(style, first, second);
    public static void Line(Style style, Point first, Point second, params ReadOnlySpan<Point> points) => Canvas.Line(style, first, second, points);
    public static void Rectangle(Style style, Point topLeft, Size size) => Canvas.Rectangle(style, topLeft, size);
    public static void Circle(Style style, Point center, Number radius) => Canvas.Circle(style, center, radius);
    public static void Ellipse(Style style, Point center, Number radiusX, Number radiusY) => Canvas.Ellipse(style, center, radiusX, radiusY);
    public static void Bezier(Style style, Point p0, Point p1, Point p2, Point p3) => Canvas.Bezier(style, p0, p1, p2, p3);
    public static void Bezier(Style style, Point p0, Point p1, Point p2, Point p3, params ReadOnlySpan<Point> points) => Canvas.Bezier(style, p0, p1, p2, p3, points);
}
