namespace QuickGraphics;

public static class QgConsole
{
    internal enum LogType { Print, Error }

    internal delegate void LogHandler(LogType logType, string message);

    internal static LogHandler LogResolver { get; set; } = (type, message) =>
    {
        switch (type)
        {
            case LogType.Error:
                Console.Error.WriteLine(message);
                break;
            case LogType.Print:
            default:
                Console.WriteLine(message);
                break;
        }
    };

    public static void Print(string message)
    {
        LogResolver(LogType.Print, message);
    }

    public static void Error(string message)
    {
        LogResolver(LogType.Error, message);
    }
}
