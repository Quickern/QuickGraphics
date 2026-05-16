using System.Numerics;
using System.Reflection;
using QuickGraphics.Mathematics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace QuickGraphics.Drawing;

public class WindowCanvas : Canvas
{
    private readonly IWindow _window;

    private IInputContext? _input;
    private IInputContext Input => _input ?? throw new NoCanvasException();

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

    private void OnLoad()
    {
        _input = _window.CreateInput();

        foreach (IMouse mouse in _input.Mice)
        {
            mouse.MouseDown += OnMouseDown;
            mouse.MouseMove += OnMouseMove;
            mouse.MouseUp += OnMouseUp;
        }

        if (_input.Mice.Any())
        {
            MouseHandler.SetRawPosition(Point.FromNumerics(_input.Mice[0].Position));
        }

        foreach (IKeyboard keyboard in _input.Keyboards)
        {
            keyboard.KeyDown += OnKeyDown;
            keyboard.KeyChar += OnKeyChar;
            keyboard.KeyUp += OnKeyUp;
        }

        Load(_window.CreateOpenGL());
    }

    private void OnRender(double _DeltaTime)
    {
        Vector2D<int> winSize = _window.Size;
        Vector2D<int> fbSize = _window.FramebufferSize;

        Render(new FrameData(new Size(winSize.X, winSize.Y), new Size(fbSize.X, fbSize.Y)));
    }

    void OnMouseDown(IMouse mouse, MouseButton mouseButton) => MouseHandler.Press(mouseButton);
    void OnMouseMove(IMouse mouse, Vector2 position) => MouseHandler.SetRawPosition(Point.FromNumerics(position));
    void OnMouseUp(IMouse mouse, MouseButton mouseButton) => MouseHandler.Release(mouseButton);

    void OnKeyDown(IKeyboard keyboard, Key key, int i) => KeyboardHandler.Press(key);
    void OnKeyChar(IKeyboard keyboard, char c) => KeyboardHandler.AddChar(c);
    void OnKeyUp(IKeyboard keyboard, Key key, int i) => KeyboardHandler.Release(key);

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
