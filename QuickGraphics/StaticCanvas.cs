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

    public static Task ForFrame => s_canvas.WaitFrame;
    public static Task ForExit => s_canvas.RunTask;

    public static Starter ForCanvas(int width, int height)
    {
        s_canvas = new Canvas(new Size(width, height));

        TaskCompletionSource tcs = new TaskCompletionSource();
        
        Thread mainThread = new Thread(() =>
        {
            Thread.CurrentThread.Name = "UI Thread";

            SingleThreadSynchronizationContext context = new SingleThreadSynchronizationContext(() => { });
            SynchronizationContext.SetSynchronizationContext(context);

            tcs.TrySetResult();

            while (!s_canvas.IsClosed)
            {
                s_canvas.FrameEvent.WaitOne();

                context.Invoke();
            }
        });
        mainThread.Start();

        return new Starter(tcs.Task);
    }

    public readonly struct Starter(Task task) : INotifyCompletion
    {
        public bool IsCompleted { get; }

        readonly Task _task = task;
        public void OnCompleted(Action _Continuation)
        {
            _task.ContinueWith((_, _) => _Continuation(), TaskContinuationOptions.ExecuteSynchronously);

            s_canvas.Run();
        }

        public void GetResult() { }

        public Starter GetAwaiter() => this;
    }

    public static void ClearCanvas() => s_canvas.ClearCanvas();
    public static void Line(Color color, Point first, Point second) => s_canvas.Line(color, first, second);
    public static void Rectangle(Color color, Point topLeft, Size size) => s_canvas.Rectangle(color, topLeft, size);
    public static void Circle(Color color, Point center, int radius) => s_canvas.Circle(color, center, radius);
}
