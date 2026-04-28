namespace QuickGraphics;

public static class StaticCanvas
{
    private static Canvas? s_canvas;

    public static bool IsClosed => Canvas.IsClosed;
    public static bool IsNotClosed => !IsClosed;

    public static Canvas Canvas => s_canvas ?? throw new NoCanvasException($"Call `await {ForCanvas}(width, height);` before any draw.");

    public static Size CanvasSize => Canvas.Size;
    public static int CanvasWidth => CanvasSize.Width;
    public static int CanvasHeight => CanvasSize.Height;

    public static Task ForFrame => Canvas.ForFrame;
    public static Task ForExit => Canvas.ForExit;

    public static CanvasAwaitable ForCanvas(int width, int height)
    {
        s_canvas = new Canvas(new Size(width, height));

        TaskCompletionSource tcs = new TaskCompletionSource();

        Thread mainThread = new Thread(() =>
        {
            Thread.CurrentThread.Name = "Canvas Thread";

            SingleThreadSynchronizationContext context = new SingleThreadSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);

            tcs.TrySetResult();

            while (!s_canvas.IsClosed)
            {
                s_canvas.FrameEvent.WaitOne();

                context.Invoke();
            }
        });
        mainThread.Start();

        return new CanvasAwaitable(s_canvas, tcs.Task);
    }

    public static void Clear() => Canvas.Clear();
    public static void Clear(Color color) => Canvas.Clear(color);
    public static void Line(Color color, Point first, Point second) => Canvas.Line(color, first, second);
    public static void Rectangle(Color color, Point topLeft, Size size) => Canvas.Rectangle(color, topLeft, size);
    public static void Circle(Color color, Point center, int radius) => Canvas.Circle(color, center, radius);
}
