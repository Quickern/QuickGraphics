using NvgNET;
using NvgNET.Rendering.OpenGL;
using Silk.NET.OpenGL;

namespace QuickGraphics;

public partial class Canvas
{
    internal CanvasSynchronizationContext Context { get; }

    internal GL Gl { get => field ?? throw new InvalidOperationException("Gl called before initialization."); private set; }
    internal Nvg Nvg { get => field ?? throw new InvalidOperationException("NVG called before initialization."); private set; }

    private readonly PrimitivesDrawer _drawer;

    public bool IsClosed { get; private set; }

    private readonly TaskCompletionSource _tcs = new TaskCompletionSource();
    public Task ForExit => _tcs.Task;

    public FrameAwaitable ForFrame => new FrameAwaitable(this);

    public Size Size { get; }
    public virtual Size FramebufferSize { get; set; }

    public Canvas(CanvasSynchronizationContext context, Size size)
    {
        Context = context;
        Size = size;

        _drawer = new PrimitivesDrawer(this);

        Clear();
    }

    public virtual void Run() { }

    public void Load(GL gl)
    {
        Gl = gl;

        OpenGLRenderer nvgRenderer = new(CreateFlags.StencilStrokes | CreateFlags.Debug, Gl);
        Nvg = Nvg.Create(nvgRenderer);
    }

    public void Render()
    {
        Context.Invoke();

        Size winSize = Size;
        Size fbSize = FramebufferSize;

        double winAspect = (double)winSize.Width / winSize.Height;
        double fbAspect = (double)fbSize.Width / fbSize.Height;

        Size finalSize = fbSize;
        if (winAspect > fbAspect)
        {
            finalSize.Height = (int)(fbSize.Width / winAspect);
        }
        else if (winAspect < fbAspect)
        {
            finalSize.Width = (int)(fbSize.Height * winAspect);
        }

        float pxRatio = (float)finalSize.Width / winSize.Width;

        Gl.Viewport((fbSize.Width - finalSize.Width) / 2, (fbSize.Height - finalSize.Height) / 2, (uint)finalSize.Width, (uint)finalSize.Height);

        Nvg.BeginFrame(winSize.Width, winSize.Height, pxRatio);
        _drawer.Draw();
        Nvg.EndFrame();
    }

    public void Close()
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
}
