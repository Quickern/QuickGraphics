using System.Runtime.CompilerServices;

namespace QuickGraphics;

public readonly struct CanvasRunAwaitable : INotifyCompletion
{
    private readonly Canvas _canvas;
    private readonly SynchronizationContext? _context;

    public CanvasRunAwaitable()
    {
        throw new NoCanvasException($"You can't create {nameof(CanvasRunAwaitable)} outside of the canvas!");
    }

    internal CanvasRunAwaitable(Canvas canvas)
    {
        _canvas = canvas;
        _context = SynchronizationContext.Current;
    }

    public bool IsCompleted { get; }

    public void OnCompleted(Action _Continuation)
    {
        if (_context != null)
        {
            _context.Post(_ => _Continuation(), null);
        }
        else
        {
            _Continuation();
        }

        _canvas.Run();
    }

    public void GetResult() { }

    public CanvasRunAwaitable GetAwaiter() => this;
}
