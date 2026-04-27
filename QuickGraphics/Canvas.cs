using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NvgNET;
using NvgNET.Rendering.OpenGL;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace QuickGraphics;

public class Canvas
{
    private IWindow _window;
    private GL _gl;

    private Nvg _nvg;
    public Nvg Nvg => _nvg;

    private SingleThreadSynchronizationContext _syncContext;
    private readonly Queue<Primitive> _toDraw = new Queue<Primitive>();

    private bool _isRunning;
    private readonly TaskCompletionSource _tcs = new TaskCompletionSource();

    public bool IsClosed { get; private set; }
    public Task RunTask
    {
        get
        {
            async Task FirstRun()
            {
                await Task.Yield();
                await _tcs.Task;
            }

            return _isRunning ? _tcs.Task : FirstRun();
        }
    }

    private TaskCompletionSource _frameTcs = new TaskCompletionSource();
    public Task WaitFrame
    {
        get
        {
            async Task FirstRun()
            {
                await Task.Yield();
                await _frameTcs.Task;
            }

            return _isRunning ? _frameTcs.Task : FirstRun();
        }
    }

    public Size Size => new Size(_window.Size.X, _window.Size.Y);

    public AutoResetEvent FrameEvent { get; } = new AutoResetEvent(false);

    public Canvas(Size size)
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

    public void Run()
    {
        _window.Run();
    }

    public void ClearCanvas()
    {
        _toDraw.Clear();
    }

    public void Line(Color color, Point first, Point second)
    {
        Line line = new Line { Color = color };
        line.Initialize(first, second);

        _toDraw.Enqueue(line);
    }

    public void Circle(Color color, Point center, int radius)
    {
        Circle circle = new Circle { Color = color };
        circle.Initialize(center, radius);

        _toDraw.Enqueue(circle);
    }

    public void Rectangle(Color color, Point topLeft, Size size)
    {
        Rectangle rect = new Rectangle { Color = color };
        rect.Initialize(topLeft, size);

        _toDraw.Enqueue(rect);
    }

    private void OnLoad()
    {
        _gl = _window.CreateOpenGL();

        OpenGLRenderer nvgRenderer = new(CreateFlags.StencilStrokes | CreateFlags.Debug, _gl);
        _nvg = Nvg.Create(nvgRenderer);
    }

    private void OnRender(double _DeltaTime)
    {
        Vector2 winSize = _window.Size.As<float>().ToSystem();
        Vector2 fbSize = _window.FramebufferSize.As<float>().ToSystem();

        float pxRatio = fbSize.X / winSize.X;

        _gl.Viewport(0, 0, (uint)fbSize.X, (uint)fbSize.Y);
        _gl.ClearColor(0, 0, 0, 1);
        _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        _nvg.BeginFrame(winSize, pxRatio);

        TaskCompletionSource frameTcs = _frameTcs;        
        _frameTcs = new TaskCompletionSource();
        frameTcs.TrySetResult();
        
        FrameEvent.Set();

        foreach (Primitive primitive in _toDraw)
        {
            primitive.Draw(this);
        }
        
        _nvg.EndFrame();
    }

    private void OnClose()
    {
        IsClosed = true;
        _tcs.TrySetResult();

        _nvg.Dispose();
        _gl.Dispose();
    }

    public void Dispose()
    {
        _window.Dispose();
    }
}
