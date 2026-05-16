using Avalonia.Input;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Rendering;
using Silk.NET.OpenGL;

namespace QuickGraphics.AvaloniaQg;

public class CanvasView(Canvas canvas) : OpenGlControlBase, ICustomHitTest
{
    private readonly Canvas _canvas = canvas;

    private TopLevel? _originalTopLevel;

    public bool HitTest(Avalonia.Point point) => Bounds.Contains(point);

    protected override void OnOpenGlInit(GlInterface gl)
    {
        base.OnOpenGlInit(gl);

        _canvas.Load(GL.GetApi(gl.GetProcAddress));

        _originalTopLevel = TopLevel.GetTopLevel(this);
        if (_originalTopLevel == null)
        {
            // Something went completely wrong.
            return;
        }

        _originalTopLevel.KeyDown += OnKeyDown;
        _originalTopLevel.KeyUp += OnKeyUp;
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            // Something went completely wrong.
            return;
        }

        double scale = topLevel.RenderScaling;

        _canvas.Render(new Canvas.FrameData(new Size(Bounds.Width, Bounds.Height), new Size(Bounds.Width * scale, Bounds.Height * scale)));

        RequestNextFrameRendering();
    }

    protected override Avalonia.Size MeasureOverride(Avalonia.Size availableSize)
    {
        Avalonia.Size canvasSize = new Avalonia.Size(_canvas.Size.Width, _canvas.Size.Height);

        (double width, double height) = availableSize;

        if (canvasSize.AspectRatio > availableSize.AspectRatio)
        {
            height = availableSize.Width / canvasSize.AspectRatio;
        }
        else if (canvasSize.AspectRatio < availableSize.AspectRatio)
        {
            width = availableSize.Height * canvasSize.AspectRatio;
        }

        return new Avalonia.Size(width, height);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        PointerUpdateKind kind = e.GetCurrentPoint(this).Properties.PointerUpdateKind;

        _canvas.MouseHandler.Press(kind switch
        {
            PointerUpdateKind.LeftButtonPressed => Silk.NET.Input.MouseButton.Left,
            PointerUpdateKind.RightButtonPressed => Silk.NET.Input.MouseButton.Right,
            PointerUpdateKind.MiddleButtonPressed => Silk.NET.Input.MouseButton.Middle,
            PointerUpdateKind.XButton1Pressed => Silk.NET.Input.MouseButton.Button4,
            PointerUpdateKind.XButton2Pressed => Silk.NET.Input.MouseButton.Button5,
            _ => Silk.NET.Input.MouseButton.Unknown
        });
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        Avalonia.Point pos = e.GetPosition(this);
        _canvas.MouseHandler.SetRawPosition(new Point(pos.X, pos.Y));
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        PointerUpdateKind kind = e.GetCurrentPoint(this).Properties.PointerUpdateKind;

        _canvas.MouseHandler.Release(kind switch
        {
            PointerUpdateKind.LeftButtonReleased => Silk.NET.Input.MouseButton.Left,
            PointerUpdateKind.RightButtonReleased => Silk.NET.Input.MouseButton.Right,
            PointerUpdateKind.MiddleButtonReleased => Silk.NET.Input.MouseButton.Middle,
            PointerUpdateKind.XButton1Released => Silk.NET.Input.MouseButton.Button4,
            PointerUpdateKind.XButton2Released => Silk.NET.Input.MouseButton.Button5,
            _ => Silk.NET.Input.MouseButton.Unknown
        });
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        _canvas.KeyboardHandler.Press(e.PhysicalKey.ToSilkKey());

        if (e.KeySymbol != null)
        {
            foreach (char c in e.KeySymbol)
            {
                _canvas.KeyboardHandler.AddChar(c);
            }
        }
    }

    private void OnKeyUp(object? sender, KeyEventArgs e)
    {
        base.OnKeyUp(e);

        _canvas.KeyboardHandler.Release(e.PhysicalKey.ToSilkKey());
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        base.OnOpenGlDeinit(gl);

        _canvas.Dispose();

        if (_originalTopLevel != null)
        {
            _originalTopLevel.KeyDown -= OnKeyDown;
            _originalTopLevel.KeyUp -= OnKeyUp;
        }
    }
}
