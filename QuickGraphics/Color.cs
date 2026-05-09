using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using ColorTuple3 = (byte Red, byte Green, byte Blue);
using ColorTuple4 = (byte Red, byte Green, byte Blue, byte Alpha);

namespace QuickGraphics;

[StructLayout(LayoutKind.Explicit)]
public struct Color : IEquatable<Color>, IParsable<Color>
{
    [FieldOffset(0)] private uint _value;

    [FieldOffset(3)] public byte Red;
    [FieldOffset(2)] public byte Green;
    [FieldOffset(1)] public byte Blue;
    [FieldOffset(0)] public byte Alpha;

    public Color(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }
    public Color(byte red, byte green, byte blue) : this(red, green, blue, 255) { }
    public Color(uint value) => _value = value;

    public static bool operator ==(Color c1, Color c2) => c1._value == c2._value;
    public static bool operator !=(Color c1, Color c2) => c1._value != c2._value;

    public static implicit operator Color(uint value) => new Color(value);
    public static implicit operator Color(ColorTuple3 tuple) => new Color(tuple.Red, tuple.Green, tuple.Blue);
    public static implicit operator Color(ColorTuple4 tuple) => new Color(tuple.Red, tuple.Green, tuple.Blue, tuple.Alpha);

    public static implicit operator uint(Color color) => color._value;
    public static implicit operator ColorTuple3(Color color) => (color.Red, color.Green, color.Blue);
    public static implicit operator ColorTuple4(Color color) => (color.Red, color.Green, color.Blue, color.Alpha);

    public readonly void Deconstruct(out byte red, out byte green, out byte blue, out byte alpha)
    {
        red = Red;
        green = Green;
        blue = Blue;
        alpha = Alpha;
    }

    public readonly void Deconstruct(out byte red, out byte green, out byte blue)
    {
        red = Red;
        green = Green;
        blue = Blue;
    }

    public readonly bool Equals(Color other) => _value == other._value;
    public override readonly bool Equals(object? o) => o is Color c && Equals(c);
    public override readonly int GetHashCode() => _value.GetHashCode();

    public override readonly string ToString()
    {
        if (Colors.ColorToName.TryGetValue(this, out string? result))
        {
            return result;
        }

        return '#' + _value.ToString("X2");
    }

    public static Color Parse(string s) => Parse(s, null);
    public static Color Parse(string s, IFormatProvider? provider) => TryParse(s, provider, out Color c) ? c : throw new InvalidOperationException($"Cannot convert \"{s}\" into {typeof(Color)}");
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Color result) => TryParse(s, null, out result);
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Color result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = default;
            return false;
        }

        if (Colors.NameToColor.TryGetValue(s, out result))
        {
            return true;
        }

        ReadOnlySpan<char> strSpan = s.AsSpan();
        if (strSpan[0] == '#')
        {
            strSpan = strSpan[1..];
        }

        Span<char> span = stackalloc char[8];
        span.Fill('F');
        if (strSpan.Length is 3 or 4)
        {
            for (int i = 0; i < strSpan.Length; i++)
            {
                span[2 * i] = strSpan[i];
                span[2 * i + 1] = strSpan[i];
            }
        }
        else if (strSpan.Length is 6 or 8)
        {
            strSpan.CopyTo(span[..strSpan.Length]);
        }
        else
        {
            result = default;
            return false;
        }

        if (uint.TryParse(span, NumberStyles.HexNumber, provider, out uint value))
        {
            result = value;
            return true;
        }

        return false;
    }
}
