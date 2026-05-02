namespace QuickGraphics;

partial class Canvas
{
    public void Clear() => Clear(Colors.Black);
    public void Clear(Color color)
    {
        _drawer.Clear();
        _drawer.Enqueue(Primitives.Clear, new Clear(color));
    }

    public void Line(Style style, Point first, Point second) => _drawer.Enqueue(Primitives.Line, new Line(style, first, second));

    public void Circle(Style style, Point center, int radius) => _drawer.Enqueue(Primitives.Circle, new Circle(style, center, radius));

    public void Rectangle(Style style, Point topLeft, Size size) => _drawer.Enqueue(Primitives.Rectangle, new Rectangle(style, topLeft, size));
}
