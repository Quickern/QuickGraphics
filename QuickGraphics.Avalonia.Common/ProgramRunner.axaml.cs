using Avalonia.Threading;
using CanvasData = (System.Threading.Tasks.TaskCompletionSource<QuickGraphics.Canvas> Canvas, System.Threading.Tasks.TaskCompletionSource Task, System.Action Program);

namespace QuickGraphics.Avalonia.Common;

public partial class ProgramRunner : UserControl
{
    public ProgramRunner()
    {
        InitializeComponent();
    }

    public Task RunProgram(Action program)
    {
        (Task<Canvas> canvas, Task task) = RunAndGetCanvas(program);
        _ = CreateCanvasView(canvas);
        return task;

        async Task CreateCanvasView(Task<Canvas> canvas)
        {
            try
            {
                CanvasView view = new CanvasView(await canvas);
                MainGrid.Children.Add(view);
            }
            catch (OperationCanceledException) { }
        }
    }

    private (Task<Canvas> CanvasTask, Task ProgramTask) RunAndGetCanvas(Action program)
    {
        CanvasData data = (
            new TaskCompletionSource<Canvas>(),
            new TaskCompletionSource(),
            program
        );

        new Thread(Run).Start(data);

        return (data.Canvas.Task, data.Task.Task);
    }

    private void Run(object? state)
    {
        Thread.CurrentThread.Name = "Program Main Thread";

        CanvasData data = (CanvasData)state!;

        StaticConsole.LogResolver = message => Dispatcher.UIThread.Post(() => Print(message));

        StaticCanvas.CanvasResolver = size =>
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

            data.Canvas.SetResult(canvas);
            return canvas;
        };

        data.Program();

        data.Task.SetResult();
        data.Canvas.TrySetCanceled();
    }

    private void Print(string message)
    {
        SelectableTextBlock textBlock = new SelectableTextBlock() { Text = message, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap };
        Log.Children.Add(textBlock);

        LogScroll.ScrollToEnd();
    }
}
