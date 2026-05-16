using System.Diagnostics;
using NvgNET;
using NvgNET.Rendering.OpenGL;
using QuickGraphics.Drawing.Input;
using QuickGraphics.Mathematics;
using Silk.NET.OpenGL;
using Rect = (int X, int Y, uint Width, uint Height, float PxRatio);

namespace QuickGraphics.Drawing;

public partial class Canvas
{
    private readonly Stopwatch _time = Stopwatch.StartNew();

    private readonly PrimitivesDrawer _drawer;

    internal CanvasSynchronizationContext Context { get; }

    internal GL Gl { get => field ?? throw new InvalidOperationException("Gl called before initialization."); private set; }
    internal Nvg Nvg { get => field ?? throw new InvalidOperationException("NVG called before initialization."); private set; }

    internal Rect ViewPort { get; private set; }

    private readonly MouseHandler _mouseHandler;
    internal MouseHandler MouseHandler => _mouseHandler;
    public IMouse Mouse => _mouseHandler;

    private readonly KeyboardHandler _keyboardHandler;
    internal KeyboardHandler KeyboardHandler => _keyboardHandler;
    public IKeyboard Keyboard => _keyboardHandler;

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
        _mouseHandler = new MouseHandler(this);
        _keyboardHandler = new KeyboardHandler(this);

        Clear();
    }

    internal virtual void Run() { }

    internal void Load(GL gl)
    {
        Gl = gl;

        OpenGLRenderer nvgRenderer = new OpenGLRenderer(CreateFlags.StencilStrokes | CreateFlags.Debug, Gl);
        Nvg = Nvg.Create(nvgRenderer);
    }

    internal void Render(FrameData renderInfo)
    {
        Size canvasSize = Size;
        Size winSize = renderInfo.WindowSize;
        Size fbSize = renderInfo.FramebufferSize;

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

        ViewPort = (
            (fbSize.Width - finalSize.Width) / 2,
            (fbSize.Height - finalSize.Height) / 2,
            (uint)finalSize.Width,
            (uint)finalSize.Height,
            (float)finalSize.Width / canvasSize.Width
        );

        using MouseHandler.UpdateContext mouseCtx = _mouseHandler.BeginUpdate(renderInfo);
        using KeyboardHandler.UpdateContext keyboardCtx = _keyboardHandler.BeginUpdate();

        TimeSpan prevTime = Time;
        Time = _time.Elapsed;
        TimeSeconds = Time.TotalSeconds;
        FrameTime = (Time - prevTime).TotalSeconds;

        Context.Invoke();

        Gl.Viewport(ViewPort.X, ViewPort.Y, ViewPort.Width, ViewPort.Height);

        Nvg.BeginFrame(canvasSize.Width, canvasSize.Height, ViewPort.PxRatio);
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
