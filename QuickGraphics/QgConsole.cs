using QuickGraphics.Mathematics;

namespace QuickGraphics;

public static class QgConsole
{
    internal interface IProvider
    {
        void Print(string message);
        void Error(string message);
        string? Input(string? message);
    }

    private class DefaultProvider : IProvider
    {
        public void Print(string message) => Console.WriteLine(message);
        public void Error(string message) => Console.Error.WriteLine(message);
        public string? Input(string? message)
        {
            if (message != null)
            {
                Console.Write($"{message} ");
            }

            return Console.ReadLine();
        }
    }

    internal static IProvider Provider { get; set; } = new DefaultProvider();

    public class Line
    {
        private readonly string _string;

        internal Line(string str)
        {
            _string = str;
        }

        public override string ToString() => _string;

        public static implicit operator Number(Line line) => Number.TryParse(line._string, out Number num) ? num : 0;
        public static implicit operator float(Line line) => float.TryParse(line._string, out float num) ? num : 0;
        public static implicit operator int(Line line) => int.TryParse(line._string, out int num) ? num : 0;

        public static implicit operator string(Line line) => line._string;
    }

    public static Line Input()
    {
        return new Line(Provider.Input(null) ?? string.Empty);
    }

    public static Line Input(string message)
    {
        return new Line(Provider.Input(message) ?? string.Empty);
    }

    public static void Print(object message)
    {
        Provider.Print(message?.ToString() ?? "[null]");
    }

    public static void Error(string message)
    {
        Provider.Error(message);
    }
}
