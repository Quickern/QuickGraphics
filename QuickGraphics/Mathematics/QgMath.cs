using System.Runtime.CompilerServices;

namespace QuickGraphics.Mathematics;

public static class QgMath
{
    public const float E = float.E;
    public const float Pi = float.Pi;
    public const float Tau = float.Tau;
    public const float Epsilon = float.Epsilon;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number DegreesToRadians(Number x) => float.DegreesToRadians(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number RadiansToDegrees(Number x) => float.RadiansToDegrees(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Cos(Number x) => MathF.Cos(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Sin(Number x) => MathF.Sin(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Min(Number x, Number y) => MathF.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Max(Number x, Number y) => MathF.Max(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Pow(Number x, Number y) => MathF.Pow(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Sqrt(Number x) => MathF.Sqrt(x);
}
