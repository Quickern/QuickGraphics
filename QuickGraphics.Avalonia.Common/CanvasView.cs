using Avalonia.Input;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Rendering;
using Silk.NET.OpenGL;

namespace QuickGraphics.Avalonia.Common;

public class CanvasView(Canvas canvas) : OpenGlControlBase, ICustomHitTest
{
    private readonly Canvas _canvas = canvas;

    private global::Avalonia.Point _mousePosition;

    public bool HitTest(global::Avalonia.Point point) => Bounds.Contains(point);

    protected override void OnOpenGlInit(GlInterface gl)
    {
        base.OnOpenGlInit(gl);

        _canvas.Load(GL.GetApi(gl.GetProcAddress));
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        double scale = TopLevel.GetTopLevel(this).RenderScaling;

        _canvas.PrepareMouse(new Point((int)_mousePosition.X, (int)_mousePosition.Y));

        _canvas.Prepare(new Canvas.FrameData(new Size((int)Bounds.Width, (int)Bounds.Height), new Size((int)(Bounds.Width * scale), (int)(Bounds.Height * scale))));

        _canvas.Render();

        RequestNextFrameRendering();
    }

    protected override global::Avalonia.Size MeasureOverride(global::Avalonia.Size availableSize)
    {
        global::Avalonia.Size canvasSize = new global::Avalonia.Size(_canvas.Size.Width, _canvas.Size.Height);

        (double width, double height) = availableSize;

        if (canvasSize.AspectRatio > availableSize.AspectRatio)
        {
            height = availableSize.Width / canvasSize.AspectRatio;
        }
        else if (canvasSize.AspectRatio < availableSize.AspectRatio)
        {
            width = availableSize.Height * canvasSize.AspectRatio;
        }

        return new global::Avalonia.Size(width, height);
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
