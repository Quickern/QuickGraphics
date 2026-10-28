using System.Diagnostics;
using System.Windows.Input;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using Avalonia.Threading;

namespace QuickGraphics.AvaloniaQg;

public partial class LogView : UserControl, QgConsole.IProvider
{
    private TaskCompletionSource<string>? _textEnterTcs;

    public ICommand EnterCommand { get; }

    public LogView()
    {
        EnterCommand = new SimpleCommand(OnEnter);

        InitializeComponent();

        DataContext = this;
    }

    public void Print(string message)
    {
        Dispatcher.UIThread.Post(() =>
        {
            SelectableTextBlock textBlock = new SelectableTextBlock() { Text = message, TextWrapping = TextWrapping.Wrap };
            Log.Children.Add(textBlock);

            LogScroll.ScrollToEnd();
        });
    }

    public void Error(string message)
    {
        Dispatcher.UIThread.Post(() =>
        {
            SelectableTextBlock textBlock = new SelectableTextBlock() { Text = message, TextWrapping = TextWrapping.Wrap };
            TextElement.SetForeground(textBlock, Brushes.Red);
            Log.Children.Add(textBlock);

            LogScroll.ScrollToEnd();
        });
    }

    public string? Input(string? message)
    {
        return Dispatcher.UIThread.InvokeAsync<string?>(async () =>
        {
            ConsoleInputText.Text = message != null ? $"> {message} " : "> ";

            ConsoleInputGrid.IsVisible = true;
            ConsoleInput.Text = string.Empty;
            ConsoleInput.Focus();

            _textEnterTcs = new TaskCompletionSource<string>();

            return await _textEnterTcs.Task;
        }).Result;
    }

    private void OnEnter()
    {
        if (_textEnterTcs == null)
        {
            return;
        }

        Debug.Assert(ConsoleInput != null && ConsoleInputGrid != null && ConsoleInputText != null);

        string inputStr = ConsoleInput.Text ?? string.Empty;

        SelectableTextBlock textBlock = new SelectableTextBlock() { Text = ConsoleInputText.Text + inputStr, TextWrapping = TextWrapping.Wrap, FontStyle = FontStyle.Italic };
        TextElement.SetForeground(textBlock, Brushes.Gray);
        Log.Children.Add(textBlock);

        LogScroll.ScrollToEnd();

        _textEnterTcs.SetResult(inputStr);

        ConsoleInput.Text = string.Empty;
        ConsoleInputGrid.IsVisible = false;
    }

    private record SimpleCommand(Action Action) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => Action();
    }
}
