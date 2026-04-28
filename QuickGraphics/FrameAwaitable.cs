using System.Runtime.CompilerServices;

namespace QuickGraphics;

public struct FrameAwaitable
{
    private readonly Canvas _canvas;

    internal FrameAwaitable(Canvas canvas)
    {
        _canvas = canvas;
    }

    public FrameAwaiter GetAwaiter() => new FrameAwaiter(_canvas);

    public struct FrameAwaiter(Canvas canvas) : INotifyCompletion
    {
        private readonly Canvas _canvas = canvas;

        public bool IsCompleted { get; }

        public void OnCompleted(Action continuation)
        {
            SynchronizationContext? syncCtx = SynchronizationContext.Current;
            if (syncCtx == _canvas.Context)
            {
                syncCtx.Post(_ => continuation(), null);
            }
            else
            {
                _canvas.Context.Post(state =>
                {
                    SynchronizationContext? syncCtx = (SynchronizationContext?)state;

                    if (syncCtx != null && syncCtx.GetType() != typeof(SynchronizationContext))
                    {
                        syncCtx.Post(_ => continuation(), null);
                    }
                    else
                    {
                        TaskScheduler scheduler = TaskScheduler.Current;
                        if (scheduler == TaskScheduler.Default)
                        {
                            ThreadPool.QueueUserWorkItem(_ => continuation(), null);
                        }
                        else
                        {
                            Task.Factory.StartNew(continuation, default, TaskCreationOptions.PreferFairness, scheduler);
                        }
                    }
                }, syncCtx);
            }
        }

        public void GetResult() { }
    }
}
