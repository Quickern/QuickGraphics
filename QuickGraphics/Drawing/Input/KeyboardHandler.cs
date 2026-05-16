using System.Runtime.InteropServices;
using Silk.NET.Input;

namespace QuickGraphics.Drawing.Input;

public class KeyboardHandler(Canvas canvas) : IKeyboard
{
    private readonly Canvas _canvas = canvas;

    private readonly HashSet<Key> _down = [];
    private readonly HashSet<Key> _pressed = [];
    private readonly HashSet<Key> _up = [];
    private readonly List<char> _enteredText = [];

    private string? _text;
    public string Text => _text ??= new string(TextSpan);

    public ReadOnlySpan<char> TextSpan => CollectionsMarshal.AsSpan(_enteredText);

    public bool IsPressed(Key key) => _pressed.Contains(key);
    public bool WasPressedDown(Key key) => _down.Contains(key);
    public bool WasReleased(Key key) => _up.Contains(key);

    public UpdateContext BeginUpdate()
    {
        return new UpdateContext(this);
    }

    public void EndUpdate()
    {
        _down.Clear();
        _up.Clear();
        _text = null;
        _enteredText.Clear();
    }

    public void Press(Key key)
    {
        _down.Add(key);
        _pressed.Add(key);
    }

    public void AddChar(char c)
    {
        _enteredText.Add(c);
    }

    public void Release(Key key)
    {
        _pressed.Remove(key);
        _up.Add(key);
    }

    public ref struct UpdateContext(KeyboardHandler handler) : IDisposable
    {
        public void Dispose() => handler.EndUpdate();
    }
}
