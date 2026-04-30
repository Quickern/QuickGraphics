using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Silk.NET.OpenGL;

using CanvasData = (System.Threading.Tasks.TaskCompletionSource<QuickGraphics.Canvas> Canvas, System.Action Program);

namespace QuickGraphics.Avalonia.Common;

public class CanvasView(Canvas canvas) : OpenGlControlBase
{
    private readonly Canvas _canvas = canvas;

    public static async Task<CanvasView> RunProgram(Action program)
    {
        TaskCompletionSource<Canvas> tcs = new TaskCompletionSource<Canvas>();

        new Thread(state =>
        {
            Thread.CurrentThread.Name = "Program Main Thread";

            (TaskCompletionSource<Canvas> tcs, Action program) = (CanvasData)state!;

            StaticCanvas.CanvasResolver = size =>
            {
                ThreadedCanvasSynchronizationContext context = new ThreadedCanvasSynchronizationContext();

                Canvas canvas = new Canvas(context, size);

                new Thread(() =>
                {
                    Thread.CurrentThread.Name = "Canvas Thread";

                    SynchronizationContext.SetSynchronizationContext(context);

                    while (!canvas.IsClosed)
                    {
                        context.WaitAndInvoke();
                    }
                }).Start();

                tcs.SetResult(canvas);
                return canvas;
            };

            program();
        }).Start((tcs, program));

        return new CanvasView(await tcs.Task);
    }

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
