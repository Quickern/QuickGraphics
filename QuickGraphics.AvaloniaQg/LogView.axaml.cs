namespace QuickGraphics.AvaloniaQg;

public partial class LogView : UserControl
{
    public LogView()
    {
        InitializeComponent();
    }

    public void AddMessage(string message)
    {
        SelectableTextBlock textBlock = new SelectableTextBlock() { Text = message, TextWrapping = Avalonia.Media.TextWrapping.Wrap };
        Log.Children.Add(textBlock);

        LogScroll.ScrollToEnd();
    }
}
