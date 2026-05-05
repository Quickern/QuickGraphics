using Avalonia.Threading;

namespace QuickGraphics.Avalonia.Common;

public class QgAvaloniaProgram
{
    private readonly QgProgram.PrintHandler? _printHandler;
    private readonly QgProgram _program;

    private readonly Queue<string> _messageQueue = new Queue<string>();
    private LogView? _logView;

    public QgAvaloniaProgram(Action program, QgProgram.PrintHandler? printHandler = null)
    {
        _printHandler = printHandler;
        _program = new QgProgram(program, PrintThreaded);
    }

    public Task RunAsync() => _program.RunAsync();

    public async Task<CanvasView> GetCanvasViewAsync()
    {
        return new CanvasView(await _program.GetCanvasAsync());
    }

    public Task<LogView> GetLogViewAsync()
    {
        if (_logView == null)
        {
            _logView = new LogView();

            foreach (string message in _messageQueue)
            {
                _logView.AddMessage(message);
            }
        }

        return Task.FromResult(_logView);
    }

    private void PrintThreaded(string message) => Dispatcher.UIThread.Post(() => Print(message));

    private void Print(string message)
    {
        if (_logView != null)
        {
            _logView.AddMessage(message);
        }
        else
        {
            _messageQueue.Enqueue(message);
        }

        _printHandler?.Invoke(message);
    }
}
