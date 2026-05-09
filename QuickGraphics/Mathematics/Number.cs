using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace QuickGraphics.Mathematics;

public record struct Number(float Value) :
    IComparable,
    IConvertible,
    ISpanFormattable,
    IComparable<Number>,
    IEquatable<Number>,
    IBinaryFloatingPointIeee754<Number>,
    IMinMaxValue<Number>,
    IUtf8SpanFormattable
{
    #region IComparable

    public int CompareTo(Number other) => Value.CompareTo(other.Value);

    #endregion

    #region IConvertible

    public TypeCode GetTypeCode() => TypeCode.Single;
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)Value).ToBoolean(provider);
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)Value).ToByte(provider);
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)Value).ToChar(provider);
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)Value).ToDateTime(provider);
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)Value).ToDecimal(provider);
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)Value).ToDouble(provider);
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)Value).ToInt16(provider);
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)Value).ToInt32(provider);
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)Value).ToInt64(provider);
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)Value).ToSByte(provider);
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)Value).ToSingle(provider);
    string IConvertible.ToString(IFormatProvider? provider) => Value.ToString(provider);
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)Value).ToType(conversionType, provider);
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)Value).ToUInt16(provider);
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)Value).ToUInt32(provider);
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)Value).ToUInt64(provider);

    #endregion

    #region IParsable

    public static Number Parse(string s, IFormatProvider? provider) => float.Parse(s, provider);
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Number result)
    {
        if (!float.TryParse(s, provider, out float f))
        {
            result = default;
            return false;
        }

        result = new Number(f);
        return true;
    }

    #endregion

    #region IParsable

    public static Number Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => float.Parse(s, provider);
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Number result)
    {
        if (!float.TryParse(s, provider, out float f))
        {
            result = default;
            return false;
        }

        result = new Number(f);
        return true;
    }

    public static bool IsPow2(Number value) => float.IsPow2(value);
    public static Number Log2(Number value) => float.Log2(value);
    public static Number Atan2(Number y, Number x) => float.Atan2(y, x);
    public static Number Atan2Pi(Number y, Number x) => float.Atan2Pi(y, x);
    public static Number BitDecrement(Number x) => float.BitDecrement(x);
    public static Number BitIncrement(Number x) => float.BitIncrement(x);
    public static Number FusedMultiplyAdd(Number left, Number right, Number addend) => float.FusedMultiplyAdd(left, right, addend);
    public static Number Ieee754Remainder(Number left, Number right) => float.Ieee754Remainder(left, right);
    public static int ILogB(Number x) => float.ILogB(x);
    public static Number ScaleB(Number x, int n) => float.ScaleB(x, n);
    public static Number Exp(Number x) => float.Exp(x);
    public static Number Exp10(Number x) => float.Exp10(x);
    public static Number Exp2(Number x) => float.Exp2(x);

    int IFloatingPoint<Number>.GetExponentByteCount() => ((IFloatingPoint<float>)Value).GetExponentByteCount();
    int IFloatingPoint<Number>.GetExponentShortestBitLength() => ((IFloatingPoint<float>)Value).GetExponentShortestBitLength();
    int IFloatingPoint<Number>.GetSignificandBitLength() => ((IFloatingPoint<float>)Value).GetSignificandBitLength();
    int IFloatingPoint<Number>.GetSignificandByteCount() => ((IFloatingPoint<float>)Value).GetSignificandByteCount();

    bool IFloatingPoint<Number>.TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten) => ((IFloatingPoint<float>)Value).TryWriteExponentBigEndian(destination, out bytesWritten);
    bool IFloatingPoint<Number>.TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten) => ((IFloatingPoint<float>)Value).TryWriteExponentLittleEndian(destination, out bytesWritten);
    bool IFloatingPoint<Number>.TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten) => ((IFloatingPoint<float>)Value).TryWriteSignificandBigEndian(destination, out bytesWritten);
    bool IFloatingPoint<Number>.TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten) => ((IFloatingPoint<float>)Value).TryWriteSignificandLittleEndian(destination, out bytesWritten);

    public static Number Round(Number x, int digits, MidpointRounding mode) => float.Round(x, digits, mode);

    public int CompareTo(object? obj) => Value.CompareTo(obj);
    public static Number Acosh(Number x) => float.Acosh(x);
    public static Number Asinh(Number x) => float.Asinh(x);
    public static Number Atanh(Number x) => float.Atanh(x);
    public static Number Cosh(Number x) => float.Cosh(x);
    public static Number Sinh(Number x) => float.Sinh(x);
    public static Number Tanh(Number x) => float.Tanh(x);
    public static Number Log(Number x) => float.Log(x);
    public static Number Log(Number x, Number newBase) => float.Log(x, newBase);
    public static Number Log10(Number x) => float.Log10(x);
    public static Number Pow(Number x, Number y) => float.Pow(x, y);
    public static Number Cbrt(Number x) => float.Cbrt(x);
    public static Number Hypot(Number x, Number y) => float.Hypot(x, y);
    public static Number RootN(Number x, int n) => float.RootN(x, n);
    public static Number Sqrt(Number x) => float.Sqrt(x);
    public static Number Acos(Number x) => float.Acos(x);
    public static Number AcosPi(Number x) => float.AcosPi(x);
    public static Number Asin(Number x) => float.Asin(x);
    public static Number AsinPi(Number x) => float.AsinPi(x);
    public static Number Atan(Number x) => float.Atan(x);
    public static Number AtanPi(Number x) => float.AtanPi(x);
    public static Number Cos(Number x) => float.Cos(x);
    public static Number CosPi(Number x) => float.CosPi(x);
    public static Number Sin(Number x) => float.Sin(x);
    public static (Number Sin, Number Cos) SinCos(Number x) => float.SinCos(x);
    public static (Number SinPi, Number CosPi) SinCosPi(Number x) => float.SinCosPi(x);
    public static Number SinPi(Number x) => float.SinPi(x);
    public static Number Tan(Number x) => float.Tan(x);
    public static Number TanPi(Number x) => float.TanPi(x);
    public static Number Abs(Number value) => float.Abs(value);
    public static bool IsEvenInteger(Number value) => float.IsEvenInteger(value);
    public static bool IsFinite(Number value) => float.IsFinite(value);
    public static bool IsInfinity(Number value) => float.IsInfinity(value);
    public static bool IsInteger(Number value) => float.IsInteger(value);
    public static bool IsNaN(Number value) => float.IsNaN(value);
    public static bool IsNegative(Number value) => float.IsNegative(value);
    public static bool IsNegativeInfinity(Number value) => float.IsNegativeInfinity(value);
    public static bool IsNormal(Number value) => float.IsNormal(value);
    public static bool IsOddInteger(Number value) => float.IsOddInteger(value);
    public static bool IsPositive(Number value) => float.IsPositive(value);
    public static bool IsPositiveInfinity(Number value) => float.IsPositiveInfinity(value);
    public static bool IsRealNumber(Number value) => float.IsRealNumber(value);
    public static bool IsSubnormal(Number value) => float.IsSubnormal(value);
    public static bool IsZero(Number value) => value.Value == 0.0f;
    public static Number MaxMagnitude(Number x, Number y) => float.MaxMagnitude(x, y);
    public static Number MaxMagnitudeNumber(Number x, Number y) => float.MaxMagnitudeNumber(x, y);
    public static Number MinMagnitude(Number x, Number y) => float.MinMagnitude(x, y);
    public static Number MinMagnitudeNumber(Number x, Number y) => float.MinMagnitudeNumber(x, y);

    public static Number Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) => float.Parse(s, style, provider);
    public static Number Parse(string s, NumberStyles style, IFormatProvider? provider) => float.Parse(s, style, provider);

    static bool INumberBase<Number>.TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out Number result) => throw new NotImplementedException();
    static bool INumberBase<Number>.TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out Number result) => throw new NotImplementedException();
    static bool INumberBase<Number>.TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out Number result) => throw new NotImplementedException();
    static bool INumberBase<Number>.TryConvertToChecked<TOther>(Number value, [MaybeNullWhen(false)] out TOther result) => throw new NotImplementedException();
    static bool INumberBase<Number>.TryConvertToSaturating<TOther>(Number value, [MaybeNullWhen(false)] out TOther result) => throw new NotImplementedException();
    static bool INumberBase<Number>.TryConvertToTruncating<TOther>(Number value, [MaybeNullWhen(false)] out TOther result) => throw new NotImplementedException();

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Number result)
    {
        if (!float.TryParse(s, style, provider, out float f))
        {
            result = default;
            return false;
        }

        result = f;
        return true;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Number result)
    {
        if (!float.TryParse(s, style, provider, out float f))
        {
            result = default;
            return false;
        }

        result = f;
        return true;
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) => Value.TryFormat(destination, out charsWritten, format, provider);
    public string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);

    #endregion

    #region IEEEE Number

    static Number IFloatingPointIeee754<Number>.Epsilon => float.Epsilon;
    static Number IFloatingPointIeee754<Number>.NaN => float.NaN;
    static Number IFloatingPointIeee754<Number>.NegativeInfinity => float.NegativeInfinity;
    static Number IFloatingPointIeee754<Number>.NegativeZero => float.NegativeZero;
    static Number IFloatingPointIeee754<Number>.PositiveInfinity => float.PositiveInfinity;

    static Number ISignedNumber<Number>.NegativeOne => -1.0f;

    static Number IFloatingPointConstants<Number>.E => float.E;
    static Number IFloatingPointConstants<Number>.Pi => float.Pi;
    static Number IFloatingPointConstants<Number>.Tau => float.Pi;

    static bool INumberBase<Number>.IsCanonical(Number value) => true;
    static bool INumberBase<Number>.IsComplexNumber(Number value) => false;
    static bool INumberBase<Number>.IsImaginaryNumber(Number value) => false;
    static Number INumberBase<Number>.Zero => 0;
    static Number INumberBase<Number>.One => 1.0f;
    static int INumberBase<Number>.Radix => 2;

    static Number IAdditiveIdentity<Number, Number>.AdditiveIdentity => 0.0f;
    static Number IMultiplicativeIdentity<Number, Number>.MultiplicativeIdentity => 1.0f;

    static Number IMinMaxValue<Number>.MaxValue => float.MaxValue;
    static Number IMinMaxValue<Number>.MinValue => float.MinValue;

    #endregion

    #region Conversion

    public static implicit operator int(Number number) => (int)number.Value;
    public static implicit operator float(Number number) => (float)number.Value;
    public static implicit operator double(Number number) => (double)number.Value;

    public static implicit operator Number(int number) => new Number(number);
    public static implicit operator Number(float number) => new Number(number);
    public static implicit operator Number(double number) => new Number((float)number);

    #endregion

    #region Operators

    public static bool operator >(Number left, Number right) => left.Value > right.Value;
    public static bool operator >=(Number left, Number right) => left.Value >= right.Value;
    public static bool operator <(Number left, Number right) => left.Value < right.Value;
    public static bool operator <=(Number left, Number right) => left.Value <= right.Value;

    public static Number operator *(Number left, Number right) => left.Value * right.Value;
    public static Number operator /(Number left, Number right) => left.Value / right.Value;
    public static Number operator %(Number left, Number right) => left.Value % right.Value;

    public static Number operator +(Number left, Number right) => left.Value + right.Value;
    public static Number operator -(Number left, Number right) => left.Value - right.Value;

    public static Number operator +(Number value) => +value.Value;
    public static Number operator -(Number value) => -value.Value;

    public static Number operator ++(Number value) => value.Value++;
    public static Number operator --(Number value) => value.Value--;

    #endregion

    #region Bitwise Operators

    static Number IBitwiseOperators<Number, Number, Number>.operator &(Number left, Number right)
    {
        uint bits = BitConverter.SingleToUInt32Bits(left) & BitConverter.SingleToUInt32Bits(right);
        return BitConverter.UInt32BitsToSingle(bits);
    }

    static Number IBitwiseOperators<Number, Number, Number>.operator |(Number left, Number right)
    {
        uint bits = BitConverter.SingleToUInt32Bits(left) | BitConverter.SingleToUInt32Bits(right);
        return BitConverter.UInt32BitsToSingle(bits);
    }

    static Number IBitwiseOperators<Number, Number, Number>.operator ^(Number left, Number right)
    {
        uint bits = BitConverter.SingleToUInt32Bits(left) ^ BitConverter.SingleToUInt32Bits(right);
        return BitConverter.UInt32BitsToSingle(bits);
    }

    static Number IBitwiseOperators<Number, Number, Number>.operator ~(Number value)
    {
        uint bits = ~BitConverter.SingleToUInt32Bits(value);
        return BitConverter.UInt32BitsToSingle(bits);
    }

    #endregion
}
