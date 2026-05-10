namespace QuickGraphics.AvaloniaQg;

public class QgProgram(Action program, QgProgram.PrintHandler? printHandler)
{
    public delegate void PrintHandler(string message);

    private readonly Action _program = program;
    private readonly PrintHandler? _printHandler = printHandler;

    private readonly TaskCompletionSource<Canvas> _canvasTcs = new TaskCompletionSource<Canvas>();
    private TaskCompletionSource? _programTcs;

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

        if (_printHandler != null)
        {
            QgConsole.LogResolver = Print;
        }

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

    private void Print(QgConsole.LogType type, string message)
    {
        _printHandler?.Invoke(message);
        Console.WriteLine($"[Canvas] {message}");
    }
}
