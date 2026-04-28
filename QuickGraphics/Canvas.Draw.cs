namespace QuickGraphics;

partial class Canvas
{
    public void Clear() => Clear(Colors.Black);
    public void Clear(Color color)
    {
        _drawer.Clear();
        _drawer.Enqueue(Primitives.Clear, new Clear(color));
    }

    public void Line(Color color, Point first, Point second) => _drawer.Enqueue(Primitives.Line, new Line(color, first, second));
    public void Circle(Color color, Point center, int radius) => _drawer.Enqueue(Primitives.Circle, new Circle(color, center, radius));
    public void Rectangle(Color color, Point topLeft, Size size) => _drawer.Enqueue(Primitives.Rectangle, new Rectangle(color, topLeft, size));
}
