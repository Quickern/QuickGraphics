namespace QuickGraphics.Avalonia.Common;

public class ThreadedCanvasSynchronizationContext : CanvasSynchronizationContext
{
    private readonly AutoResetEvent _start = new AutoResetEvent(false);
    private readonly AutoResetEvent _finish = new AutoResetEvent(false);

    public override void Invoke()
    {
        _start.Set();
        _finish.WaitOne();
    }

    public void WaitAndInvoke()
    {
        _start.WaitOne();
        base.Invoke();
        _finish.Set();
    }
}
