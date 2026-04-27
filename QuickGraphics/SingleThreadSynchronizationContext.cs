using System.Collections.Concurrent;

namespace QuickGraphics;

public class SingleThreadSynchronizationContext : SynchronizationContext
{
    readonly ConcurrentQueue<(SendOrPostCallback D, object? State)> m_Queue = new();
    readonly Thread m_MainThread = Thread.CurrentThread;

    public bool IsMainThread => m_MainThread == Thread.CurrentThread;

    public override void Send(SendOrPostCallback _D, object? _State)
    {
        ArgumentNullException.ThrowIfNull(_D);

        if (IsMainThread)
        {
            _D(_State);
        }
        else
        {
            using ManualResetEvent evt = new ManualResetEvent(false);
            m_Queue.Enqueue((_S => {
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

        m_Queue.Enqueue((_D, _State));
    }

    public void Invoke()
    {
        while (m_Queue.TryDequeue(out (SendOrPostCallback D, object? State) fromQueue))
        {
            fromQueue.D(fromQueue.State);
        }
    }
}
