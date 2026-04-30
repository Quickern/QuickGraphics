using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Silk.NET.OpenGL;

namespace QuickGraphics.Avalonia.Common;

public class CanvasView(Canvas canvas) : OpenGlControlBase
{
    private readonly Canvas _canvas = canvas;

    protected override void OnOpenGlInit(GlInterface gl)
    {
        base.OnOpenGlInit(gl);

        _canvas.Load(GL.GetApi(gl.GetProcAddress));
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        double scale = TopLevel.GetTopLevel(this).RenderScaling;
        _canvas.FramebufferSize = new Size((int)(Bounds.Width * scale), (int)(Bounds.Height * scale));

        _canvas.Render();

        RequestNextFrameRendering();
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        base.OnOpenGlDeinit(gl);

        _canvas.Dispose();
    }
}
