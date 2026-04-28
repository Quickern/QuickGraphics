using System.Runtime.CompilerServices;

namespace QuickGraphics;

public readonly struct CanvasRunAwaitable : INotifyCompletion
{
    private readonly Canvas _canvas;

    public CanvasRunAwaitable()
    {
        throw new NoCanvasException($"You can't create {nameof(CanvasRunAwaitable)} outside of the canvas!");
    }

    internal CanvasRunAwaitable(Canvas canvas)
    {
        _canvas = canvas;
    }

    public bool IsCompleted { get; }

    public void OnCompleted(Action _Continuation)
    {
        _Continuation();

        _canvas.Run();
    }

    public void GetResult() { }

    public CanvasRunAwaitable GetAwaiter() => this;
}
