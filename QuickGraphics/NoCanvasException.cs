namespace QuickGraphics;

public class NoCanvasException : Exception
{
    public NoCanvasException() { }

    public NoCanvasException(string? message) : base(message) { }

    public NoCanvasException(string? message, Exception? innerException) : base(message, innerException) { }
}
