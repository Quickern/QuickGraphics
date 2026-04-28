using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NvgNET;
using NvgNET.Rendering.OpenGL;
using QuickGraphics.Primitives;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace QuickGraphics;

public partial class Canvas
{
    CanvasSynchronizationContext _context;

    internal IWindow Window { get; }

    internal GL Gl { get => field ?? throw new Exception(); private set; }
    internal Nvg Nvg { get => field ?? throw new Exception(); private set; }

    private readonly Queue<Primitive> _swapchain = new Queue<Primitive>();

    public bool IsClosed { get; private set; }

    private readonly TaskCompletionSource _tcs = new TaskCompletionSource();
    public Task ForExit => _tcs.Task;

    private TaskCompletionSource _frameTcs = new TaskCompletionSource();
    public Task ForFrame => _frameTcs.Task;

    public Size Size => new Size(Window.Size.X, Window.Size.Y);

    public Canvas(Size size)
    {
        _context = new CanvasSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(_context);

        WindowOptions windowOptions = WindowOptions.Default;
        windowOptions.FramesPerSecond = 60;
        windowOptions.ShouldSwapAutomatically = true;
        windowOptions.Size = new Vector2D<int>(size.Width, size.Height);
        windowOptions.Title = Assembly.GetEntryAssembly()?.GetName().Name ?? "Canvas";
        windowOptions.VSync = false;
        windowOptions.PreferredDepthBufferBits = 24;
        windowOptions.PreferredStencilBufferBits = 8;

        Window = Silk.NET.Windowing.Window.Create(windowOptions);
        Window.Load += OnLoad;
        Window.Render += OnRender;
        Window.Closing += OnClose;

        Clear();
    }

    public void Run()
    {
        if (!Window.IsInitialized)
        {
            Window.Run();
        }
    }

    private void OnLoad()
    {
        Gl = Window.CreateOpenGL();

        OpenGLRenderer nvgRenderer = new(CreateFlags.StencilStrokes | CreateFlags.Debug, Gl);
        Nvg = Nvg.Create(nvgRenderer);
    }

    private void OnRender(double _DeltaTime)
    {
        Vector2 winSize = Window.Size.As<float>().ToSystem();
        Vector2 fbSize = Window.FramebufferSize.As<float>().ToSystem();

        float pxRatio = fbSize.X / winSize.X;

        Gl.Viewport(0, 0, (uint)fbSize.X, (uint)fbSize.Y);

        Nvg.BeginFrame(winSize, pxRatio);

        TaskCompletionSource frameTcs = _frameTcs;
        _frameTcs = new TaskCompletionSource();
        frameTcs.TrySetResult();

        _context.Invoke();

        foreach (Primitive primitive in _swapchain)
        {
            primitive.Draw(this);
        }

        Nvg.EndFrame();
    }

    private void OnClose()
    {
        IsClosed = true;

        TaskCompletionSource frameTcs = _frameTcs;
        _frameTcs = new TaskCompletionSource();
        frameTcs.TrySetResult();

        _tcs.TrySetResult();
        _context.Invoke();

        Nvg.Dispose();
        Gl.Dispose();
    }

    public void Dispose()
    {
        Window.Dispose();
    }
}
