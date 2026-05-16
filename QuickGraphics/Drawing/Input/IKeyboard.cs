using Silk.NET.Input;

namespace QuickGraphics.Drawing.Input;

public interface IKeyboard
{
    string Text { get; }
    ReadOnlySpan<char> TextSpan { get; }

    bool IsPressed(Key key);

    bool WasPressedDown(Key key);
    bool WasReleased(Key key);
}
