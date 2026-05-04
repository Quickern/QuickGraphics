namespace QuickGraphics;

partial class Canvas
{
    public void Clear() => Clear(Colors.Black);
    public void Clear(Color color)
    {
        _drawer.Clear();
        _drawer.Enqueue(Primitives.Clear, new Clear(color));
    }

    public void Line(Style style, Point first, Point second) => _drawer.Enqueue(Primitives.Line, new Line(style, first, second, 0));
    public void Line(Style style, Point first, Point second, params ReadOnlySpan<Point> points) => _drawer.Enqueue(Primitives.Line, new Line(style, first, second, points.Length), ref points);
    public void Rectangle(Style style, Point topLeft, Size size) => _drawer.Enqueue(Primitives.Rectangle, new Rectangle(style, topLeft, size));
    public void Circle(Style style, Point center, int radius) => _drawer.Enqueue(Primitives.Circle, new Circle(style, center, radius));
    public void Ellipse(Style style, Point center, int radiusX, int radiusY) => _drawer.Enqueue(Primitives.Ellipse, new Ellipse(style, center, radiusX, radiusY));
    public void Bezier(Style style, Point p0, Point p1, Point p2, Point p3) => _drawer.Enqueue(Primitives.Bezier, new Bezier(style, p0, p1, p2, p3, 0));
    public void Bezier(Style style, Point p0, Point p1, Point p2, Point p3, params ReadOnlySpan<Point> points) => _drawer.Enqueue(Primitives.Bezier, new Bezier(style, p0, p1, p2, p3, points.Length), ref points);
}
