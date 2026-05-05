namespace QuickGraphics.Avalonia.Common;

public partial class LogView : UserControl
{
    public LogView()
    {
        InitializeComponent();
    }

    public void AddMessage(string message)
    {
        SelectableTextBlock textBlock = new SelectableTextBlock() { Text = message, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap };
        Log.Children.Add(textBlock);

        LogScroll.ScrollToEnd();
    }
}
