using System.Runtime.CompilerServices;

namespace QuickGraphics;

public readonly struct CanvasAwaitable : INotifyCompletion
{
    private readonly Canvas _canvas;
    private readonly Task _task;

    public CanvasAwaitable()
    {
        throw new ArgumentException($"You can't create {nameof(CanvasAwaitable)} outside of the canvas!");
    }

    internal CanvasAwaitable(Canvas canvas, Task task)
    {
        _canvas = canvas;
        _task = task;
    }

    public bool IsCompleted { get; }

    public void OnCompleted(Action _Continuation)
    {
        _task.ContinueWith((_, _) => _Continuation(), TaskContinuationOptions.ExecuteSynchronously);

        _canvas.Run();
    }

    public void GetResult() { }

    public CanvasAwaitable GetAwaiter() => this;
}
