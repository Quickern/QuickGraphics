using System.Reflection;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace QuickGraphics;

public class WindowCanvas : Canvas
{
    private readonly IWindow _window;

    public override Size FramebufferSize
    {
        get
        {
            Vector2D<int> size = _window.FramebufferSize;
            return new Size(size.X, size.Y);
        }
        set => throw new InvalidOperationException();
    }

    public WindowCanvas(Size size) : base(SetupSynchronizationContext(), size)
    {
        WindowOptions windowOptions = WindowOptions.Default;
        windowOptions.FramesPerSecond = 60;
        windowOptions.ShouldSwapAutomatically = true;
        windowOptions.Size = new Vector2D<int>(size.Width, size.Height);
        windowOptions.Title = Assembly.GetEntryAssembly()?.GetName().Name ?? "Canvas";
        windowOptions.VSync = false;
        windowOptions.PreferredDepthBufferBits = 24;
        windowOptions.PreferredStencilBufferBits = 8;

        _window = Window.Create(windowOptions);
        _window.Load += OnLoad;
        _window.Render += OnRender;
        _window.Closing += OnClose;
    }

    public override void Run()
    {
        if (!_window.IsInitialized)
        {
            _window.Run();
        }
    }

    private static CanvasSynchronizationContext SetupSynchronizationContext()
    {
        CanvasSynchronizationContext context = new CanvasSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(context);
        return context;
    }

    private void OnRender(double _DeltaTime)
    {
        Render();
    }

    private void OnLoad()
    {
        Load(_window.CreateOpenGL());
    }

    private void OnClose()
    {
        Close();

        Gl.Dispose();
    }

    public override void Dispose()
    {
        base.Dispose();

        _window.Dispose();
    }
}
