using System.Runtime.CompilerServices;

namespace QuickGraphics;

public static class StaticCanvas
{
    private static Canvas? s_canvas;

    public static bool IsClosed => s_canvas.IsClosed;
    public static bool IsNotClosed => !IsClosed;

    public static Size CanvasSize => s_canvas.Size;
    public static int CanvasWidth => CanvasSize.Width;
    public static int CanvasHeight => CanvasSize.Height;

    public static YieldAwaitable ForFrame => s_canvas.WaitFrame;
    public static Task ForExit => s_canvas.RunTask;

    public static void Canvas(int width, int height)
    {
        s_canvas = new Canvas(new Size(width, height));
    }

    public static void Line(Color color, Point first, Point second) => s_canvas.Line(color, first, second);
    public static void Rectangle(Color color, Vector topLeft, Vector bottomRight) { }
    public static void Circle(Color color, Vector center, int radius) { }
}
