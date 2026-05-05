using System.Numerics;
using System.Reflection;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace QuickGraphics;

public class WindowCanvas : Canvas
{
    private readonly IWindow _window;
    private IInputContext _input;

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

    internal override void Run()
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
        Vector2 mp = _input.Mice[0].Position;

        mp.X *= (float)Size.Width / _window.Size.X;
        mp.Y *= (float)Size.Height / _window.Size.Y;

        Mouse = new MouseData(new Point((int)mp.X, (int)mp.Y));

        Vector2D<int> winSize = _window.Size;
        Vector2D<int> fbSize = _window.FramebufferSize;
        Prepare(new FrameData(new Size(winSize.X, winSize.Y), new Size(fbSize.X, fbSize.Y)));

        Render();
    }

    private void OnLoad()
    {
        _input = _window.CreateInput();

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
