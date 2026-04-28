using QuickGraphics.Primitives;

namespace QuickGraphics;

partial class Canvas
{
    public void Clear() => Clear(Colors.Black);
    public void Clear(Color color)
    {
        _swapchain.Clear();

        Clear clear = new Clear();
        clear.Initialize(color);

        _swapchain.Enqueue(clear);
    }

    public void Line(Color color, Point first, Point second)
    {
        Line line = new Line { Color = color };
        line.Initialize(first, second);

        _swapchain.Enqueue(line);
    }

    public void Circle(Color color, Point center, int radius)
    {
        Circle circle = new Circle { Color = color };
        circle.Initialize(center, radius);

        _swapchain.Enqueue(circle);
    }

    public void Rectangle(Color color, Point topLeft, Size size)
    {
        Rectangle rect = new Rectangle { Color = color };
        rect.Initialize(topLeft, size);

        _swapchain.Enqueue(rect);
    }
}
