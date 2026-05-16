using QuickGraphics.Mathematics;
using Silk.NET.Input;

namespace QuickGraphics.Drawing.Input;

public interface IMouse
{
    Point Position { get; }


    bool IsPressed(MouseButton mouseButton);

    bool WasPressedDown(MouseButton mouseButton);
    bool WasRelease(MouseButton mouseButton);
}
