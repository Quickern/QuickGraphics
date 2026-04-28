using System.Buffers;
using System.Runtime.InteropServices;

namespace QuickGraphics;

internal class PrimitivesDrawer(Canvas canvas)
{
    private readonly Canvas _canvas = canvas;

    private readonly List<byte> _queue = new List<byte>(4096);

    public void Clear()
    {
        _queue.Clear();
    }

    public void Enqueue<T>(byte id, T value) where T : unmanaged
    {
        _queue.Add(id);

        ReadOnlySpan<byte> data = MemoryMarshal.AsBytes([ value ]);
        _queue.AddRange(data);
    }

    public void Draw()
    {
        ReadOnlySpan<byte> span = CollectionsMarshal.AsSpan(_queue);
        for (int i = 0; i < span.Length;)
        {
            byte id = span[i];
            i++;

            (int size, Primitives.Handler draw) = Primitives.Handlers[id];

            ReadOnlySpan<byte> data = span[i..(i+size)];
            i += size;
            draw(_canvas, ref data);
        }
    }
}
