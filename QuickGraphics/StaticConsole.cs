namespace QuickGraphics;

public static class StaticConsole
{
    internal static Action<string> LogResolver { get; set; } = message => Console.WriteLine(message);

    public static void Print(string message)
    {
        LogResolver(message);
    }
}
