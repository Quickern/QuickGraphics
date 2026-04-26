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
    private readonly TaskCompletionSource m_Tcs = new TaskCompletionSource();

    public bool IsClosed { get; private set; }
    public Task RunTask
    {
        get
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _window.Run();
            }
            
            return m_Tcs.Task;
        }
    }

    public YieldAwaitable WaitFrame
    {
        get
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _window.Run();
            }

            return Task.Yield();
        }
    }

    public Size Size => new Size(_window.Size.X, _window.Size.Y);

    public Canvas(Size size)
    {
        _syncContext = new SingleThreadSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(_syncContext);

        WindowOptions windowOptions = WindowOptions.Default;
        windowOptions.FramesPerSecond = -1;
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
        
        _syncContext.Invoke();

        foreach (Primitive primitive in _toDraw)
        {
            primitive.Draw(this);
        }
        
        _nvg.EndFrame();
    }

    private void OnClose()
    {
        IsClosed = true;
        m_Tcs.TrySetResult();

        _nvg.Dispose();
        _gl.Dispose();
    }

    public void Dispose()
    {
        _window.Dispose();
    }
}
