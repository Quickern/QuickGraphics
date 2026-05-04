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
        ReadOnlySpan<byte> additional = ReadOnlySpan<byte>.Empty;
        Enqueue(id, value, ref additional);
    }

    public void Enqueue<T, TData>(byte id, T value, ref ReadOnlySpan<TData> additionalData)
        where T : unmanaged
        where TData : unmanaged
    {
        ReadOnlySpan<byte> additional = MemoryMarshal.AsBytes(additionalData);
        Enqueue(id, value, ref additional);
    }

    public void Enqueue<T>(byte id, T value, ref readonly ReadOnlySpan<byte> additionalBytes) where T : unmanaged
    {
        _queue.Add(id);

        ReadOnlySpan<byte> data = MemoryMarshal.AsBytes([ value ]);
        _queue.AddRange(data);
        _queue.AddRange(additionalBytes);
    }

    public void Draw()
    {
        ReadOnlySpan<byte> data = CollectionsMarshal.AsSpan(_queue);
        for (int i = 0; i < data.Length;)
        {
            byte id = data[i];
            i++;

            Primitives.Handler draw = Primitives.Handlers[id];

            draw(_canvas, ref data, ref i);
        }
    }
}
