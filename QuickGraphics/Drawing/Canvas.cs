using System.Diagnostics;
using NvgNET;
using NvgNET.Rendering.OpenGL;
using QuickGraphics.Mathematics;
using Silk.NET.OpenGL;

namespace QuickGraphics.Drawing;

public partial class Canvas
{
    private FrameData _renderInfo;

    private readonly Stopwatch _time = Stopwatch.StartNew();

    internal CanvasSynchronizationContext Context { get; }

    internal GL Gl { get => field ?? throw new InvalidOperationException("Gl called before initialization."); private set; }
    internal Nvg Nvg { get => field ?? throw new InvalidOperationException("NVG called before initialization."); private set; }

    private readonly PrimitivesDrawer _drawer;

    public bool IsClosed { get; private set; }

    private readonly TaskCompletionSource _tcs = new TaskCompletionSource();
    public Task ForExit => _tcs.Task;

    public FrameAwaitable ForFrame => new FrameAwaitable(this);

    public TimeSpan Time { get; private set; }
    public Number TimeSeconds { get; private set; }
    public Number FrameTime { get; private set; }

    public Size Size { get; }
    public int Width => Size.Width;
    public int Height => Size.Height;

    public Canvas(CanvasSynchronizationContext context, Size size)
    {
        Context = context;
        Size = size;

        _drawer = new PrimitivesDrawer(this);

        Clear();
    }

    internal virtual void Run() { }

    internal void Load(GL gl)
    {
        Gl = gl;

        OpenGLRenderer nvgRenderer = new OpenGLRenderer(CreateFlags.StencilStrokes | CreateFlags.Debug, Gl);
        Nvg = Nvg.Create(nvgRenderer);
    }

    internal void Prepare(FrameData renderInfo)
    {
        _renderInfo = renderInfo;
    }

    internal void Render()
    {
        Size canvasSize = Size;
        Size winSize = _renderInfo.WindowSize;
        Size fbSize = _renderInfo.FramebufferSize;

        double canvasAspect = (double)canvasSize.Width / canvasSize.Height;
        double fbAspect = (double)fbSize.Width / fbSize.Height;

        Size finalSize = fbSize;
        if (canvasAspect > fbAspect)
        {
            finalSize.Height = (int)(fbSize.Width / canvasAspect);
        }
        else if (canvasAspect < fbAspect)
        {
            finalSize.Width = (int)(fbSize.Height * canvasAspect);
        }

        float pxRatio = (float)finalSize.Width / canvasSize.Width;

        (int X, int Y, uint Width, uint Height) viewPort = (
            (fbSize.Width - finalSize.Width) / 2,
            (fbSize.Height - finalSize.Height) / 2,
            (uint)finalSize.Width,
            (uint)finalSize.Height
        );

        (double X, double Y) mouseInFb = (
            (double)_mousePosition.X * fbSize.Width / winSize.Width,
            (double)_mousePosition.Y * fbSize.Height / winSize.Height);

        Mouse = new MouseData(new Point((int)((mouseInFb.X - viewPort.X) / pxRatio),
            (int)((mouseInFb.Y - viewPort.Y) / pxRatio)));

        TimeSpan prevTime = Time;
        Time = _time.Elapsed;
        TimeSeconds = Time.TotalSeconds;
        FrameTime = (Time - prevTime).TotalSeconds;

        Context.Invoke();

        Gl.Viewport(viewPort.X, viewPort.Y, viewPort.Width, viewPort.Height);

        Nvg.BeginFrame(canvasSize.Width, canvasSize.Height, pxRatio);
        _drawer.Draw();
        Nvg.EndFrame();
    }

    internal void Close()
    {
        if (IsClosed)
        {
            return;
        }

        IsClosed = true;

        _tcs.TrySetResult();
        Context.Invoke();

        Context.Dispose();
        Nvg.Dispose();
    }

    public virtual void Dispose()
    {
        Close();
    }

    internal record struct FrameData(Size WindowSize, Size FramebufferSize);
}
