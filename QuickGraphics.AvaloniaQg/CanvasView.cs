using Avalonia.Input;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Rendering;
using Silk.NET.OpenGL;

namespace QuickGraphics.AvaloniaQg;

public class CanvasView(Canvas canvas) : OpenGlControlBase, ICustomHitTest
{
    private readonly Canvas _canvas = canvas;

    private Avalonia.Point _mousePosition;

    public bool HitTest(Avalonia.Point point) => Bounds.Contains(point);

    protected override void OnOpenGlInit(GlInterface gl)
    {
        base.OnOpenGlInit(gl);

        _canvas.Load(GL.GetApi(gl.GetProcAddress));
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            // Something goes completely wrong.
            return;
        }

        double scale = topLevel.RenderScaling;

        _canvas.PrepareMouse(new Point(_mousePosition.X, _mousePosition.Y));

        _canvas.Prepare(new Canvas.FrameData(new Size(Bounds.Width, Bounds.Height), new Size(Bounds.Width * scale, Bounds.Height * scale)));

        _canvas.Render();

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

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        _mousePosition = e.GetPosition(this);
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        base.OnOpenGlDeinit(gl);

        _canvas.Dispose();
    }
}
