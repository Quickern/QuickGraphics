namespace QuickGraphics;

public static class StaticCanvas
{
    internal static Func<Size, Canvas> CanvasResolver { get; set; } = size => new WindowCanvas(size);

    private static Canvas? s_canvas;

    public static bool IsClosed => Canvas.IsClosed;
    public static bool IsNotClosed => !IsClosed;

    public static Canvas Canvas => s_canvas ?? throw new NoCanvasException($"Call `await {ForCanvas}(width, height);` before any draw.");

    public static Size CanvasSize => Canvas.Size;
    public static int CanvasWidth => CanvasSize.Width;
    public static int CanvasHeight => CanvasSize.Height;

    public static FrameAwaitable ForFrame => Canvas.ForFrame;
    public static Task ForExit => Canvas.ForExit;

    public static CanvasRunAwaitable ForCanvas(int width, int height)
    {
        s_canvas = CanvasResolver(new Size(width, height));

        return new CanvasRunAwaitable(s_canvas);
    }

    public static void Clear() => Canvas.Clear();
    public static void Clear(Color color) => Canvas.Clear(color);

    public static void Line(Style style, Point first, Point second) => Canvas.Line(style, first, second);
    public static void Rectangle(Style style, Point topLeft, Size size) => Canvas.Rectangle(style, topLeft, size);
    public static void Circle(Style style, Point center, int radius) => Canvas.Circle(style, center, radius);
    public static void Ellipse(Style style, Point center, int radiusX, int radiusY) => Canvas.Ellipse(style, center, radiusX, radiusY);
}
