namespace QuickGraphics.AvaloniaQg;

public class QgProgram(Action program)
{
    private readonly Action _program = program;

    private readonly TaskCompletionSource<Canvas> _canvasTcs = new TaskCompletionSource<Canvas>();
    private TaskCompletionSource? _programTcs;

    // TODO: I don't like this internal!
    internal QgConsole.IProvider Provider { get => QgConsole.Provider; set => QgConsole.Provider = value; }

    public Task RunAsync()
    {
        if (_programTcs != null)
        {
            return _programTcs.Task;
        }

        _programTcs = new TaskCompletionSource();

        new Thread(MainThreadLoop).Start();

        return _programTcs.Task;
    }

    public Task<Canvas> GetCanvasAsync() => _canvasTcs.Task;

    private void MainThreadLoop(object? state)
    {
        Thread.CurrentThread.Name = "Program Main Thread";

        QgCanvas.CanvasResolver = CreateCanvas;

        _program();

        _programTcs!.SetResult();
        _canvasTcs.TrySetCanceled();
    }

    private Canvas CreateCanvas(Size size)
    {
        ThreadedCanvasSynchronizationContext context = new ThreadedCanvasSynchronizationContext();

        Canvas canvas = new Canvas(context, size);

        new Thread(() =>
        {
            Thread.CurrentThread.Name = "Canvas Thread";

            SynchronizationContext.SetSynchronizationContext(context);

            while (!canvas.IsClosed)
            {
                context.WaitAndInvoke();
            }
        }).Start();

        _canvasTcs.SetResult(canvas);
        return canvas;
    }
}
