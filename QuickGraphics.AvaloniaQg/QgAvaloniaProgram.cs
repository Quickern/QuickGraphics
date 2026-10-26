namespace QuickGraphics.AvaloniaQg;

public class QgAvaloniaProgram
{
    private readonly QgProgram _program;

    private LogView? _logView;

    public QgAvaloniaProgram(Action program)
    {
        _program = new QgProgram(program);
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
            _program.Provider = _logView;
        }

        return Task.FromResult(_logView);
    }
}
