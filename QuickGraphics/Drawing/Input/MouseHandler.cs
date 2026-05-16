using QuickGraphics.Mathematics;
using Silk.NET.Input;

namespace QuickGraphics.Drawing.Input;

internal class MouseHandler(Canvas canvas) : IMouse
{
    private readonly Canvas _canvas = canvas;

    private Point _rawPosition;

    private readonly HashSet<MouseButton> _down = [];
    private readonly HashSet<MouseButton> _pressed = [];
    private readonly HashSet<MouseButton> _up = [];

    public Point Position { get; private set; }

    public bool IsPressed(MouseButton mouseButton) => _pressed.Contains(mouseButton);
    public bool WasPressedDown(MouseButton mouseButton) => _down.Contains(mouseButton);
    public bool WasRelease(MouseButton mouseButton) => _up.Contains(mouseButton);

    public UpdateContext BeginUpdate(Canvas.FrameData renderInfo)
    {
        Size winSize = renderInfo.WindowSize;
        Size fbSize = renderInfo.FramebufferSize;

        (double X, double Y) mouseInFb = (
            (double)_rawPosition.X * fbSize.Width / winSize.Width,
            (double)_rawPosition.Y * fbSize.Height / winSize.Height);

        Position = new Point((int)((mouseInFb.X - _canvas.ViewPort.X) / _canvas.ViewPort.PxRatio),
            (int)((mouseInFb.Y - _canvas.ViewPort.Y) / _canvas.ViewPort.PxRatio));

        return new UpdateContext(this);
    }

    public void EndUpdate()
    {
        _down.Clear();
        _up.Clear();
    }

    public void SetRawPosition(Point position) => _rawPosition = position;

    public void Press(MouseButton button)
    {
        _down.Add(button);
        _pressed.Add(button);
    }

    public void Release(MouseButton button)
    {
        _pressed.Remove(button);
        _up.Add(button);
    }

    public ref struct UpdateContext(MouseHandler handler) : IDisposable
    {
        public void Dispose() => handler.EndUpdate();
    }
}
