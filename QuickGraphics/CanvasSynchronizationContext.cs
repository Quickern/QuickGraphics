using System.Collections.Concurrent;
using Frame = (System.Threading.SendOrPostCallback D, object? State);

namespace QuickGraphics;

public class CanvasSynchronizationContext : SynchronizationContext, IDisposable
{
    private readonly ConcurrentQueue<Frame> _queue = new();
    private readonly Queue<Frame> _currentThreadQueue = new();
    private readonly Thread _mainThread = Thread.CurrentThread;

    private bool _isDisposed;

    public bool IsMainThread => _mainThread == Thread.CurrentThread;

    public override void Send(SendOrPostCallback _D, object? _State)
    {
        ArgumentNullException.ThrowIfNull(_D);

        if (IsMainThread || _isDisposed)
        {
            _D(_State);
        }
        else
        {
            using ManualResetEvent evt = new ManualResetEvent(false);
            _queue.Enqueue((_S => {
                try
                {
                    _D(_S);
                }
                finally
                {
                    // ReSharper disable once AccessToDisposedClosure
                    evt.Set();
                }
            }, _State));
            evt.WaitOne();
        }
    }

    public override void Post(SendOrPostCallback _D, object? _State)
    {
        ArgumentNullException.ThrowIfNull(_D);

        if (_isDisposed)
        {
            _D(_State);
        }
        else
        {
            _queue.Enqueue((_D, _State));
        }
    }

    public virtual void Invoke()
    {
        while (_queue.TryDequeue(out Frame fromQueue))
        {
            _currentThreadQueue.Enqueue(fromQueue);
        }

        while (_currentThreadQueue.TryDequeue(out Frame fromQueue))
        {
            fromQueue.D(fromQueue.State);
        }
    }

    public void Dispose()
    {
        _isDisposed = true;
    }
}
