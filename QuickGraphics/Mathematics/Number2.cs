using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace QuickGraphics.Mathematics;

public readonly struct Number2
    : IComparable,
      IConvertible,
      ISpanFormattable,
      IComparable<Number2>,
      IEquatable<Number2>,
      IRationalNumber<Number2>,
      IMinMaxValue<Number2>,
      IUtf8SpanFormattable
{
    private readonly int _dividend;
    private readonly uint _divisor;

    public static readonly Number2 MinValue = new Number2(int.MaxValue, 0);
    public static readonly Number2 MaxValue = new Number2(int.MinValue, 0);

    private Number2(int dividend, uint divisor)
    {
        _dividend = dividend;
        _divisor = divisor;
    }

    #region IComparable

    public int CompareTo(object? value)
    {
        if (value is not Number2 number)
        {
            return (value is null) ? 1 : throw new ArgumentException("Must be a number!");
        }

        return this < number ? -1 : this > number ? 1 : 0;
    }

    #endregion

    #region IConvertible

    public TypeCode GetTypeCode() => TypeCode.Single;

    bool IConvertible.ToBoolean(IFormatProvider? provider) => this != default;

    byte IConvertible.ToByte(IFormatProvider? provider) => (byte)Round(this);
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => (sbyte)Round(this);
    short IConvertible.ToInt16(IFormatProvider? provider) => (short)Round(this);
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => (ushort)Round(this);
    int IConvertible.ToInt32(IFormatProvider? provider)  => (int)Round(this);
    uint IConvertible.ToUInt32(IFormatProvider? provider) => (uint)Round(this);
    long IConvertible.ToInt64(IFormatProvider? provider) => (long)Round(this);
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => (ulong)Round(this);

    float IConvertible.ToSingle(IFormatProvider? provider) => (float)_dividend / (_divisor + 1);
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => (decimal)_dividend / (_divisor + 1);
    double IConvertible.ToDouble(IFormatProvider? provider) => (double)_dividend / (_divisor + 1);

    char IConvertible.ToChar(IFormatProvider? provider)
    {
        if (_divisor != 0)
            throw new InvalidCastException(); // TODO: Error

        return Convert.ToChar(_dividend);
    }

    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => throw new InvalidCastException(); // TODO: Error

    object IConvertible.ToType(Type targetType, IFormatProvider? provider)
    {
        ArgumentNullException.ThrowIfNull(targetType);

        if (ReferenceEquals(typeof(Number2), targetType))
        {
            return this;
        }

        IConvertible value = this;

        if (ReferenceEquals(targetType, typeof(bool)))
            return value.ToBoolean(provider);
        if (ReferenceEquals(targetType, typeof(char)))
            return value.ToChar(provider);
        if (ReferenceEquals(targetType, typeof(sbyte)))
            return value.ToSByte(provider);
        if (ReferenceEquals(targetType, typeof(byte)))
            return value.ToByte(provider);
        if (ReferenceEquals(targetType, typeof(short)))
            return value.ToInt16(provider);
        if (ReferenceEquals(targetType, typeof(ushort)))
            return value.ToUInt16(provider);
        if (ReferenceEquals(targetType, typeof(int)))
            return value.ToInt32(provider);
        if (ReferenceEquals(targetType, typeof(uint)))
            return value.ToUInt32(provider);
        if (ReferenceEquals(targetType, typeof(long)))
            return value.ToInt64(provider);
        if (ReferenceEquals(targetType, typeof(ulong)))
            return value.ToUInt64(provider);
        if (ReferenceEquals(targetType, typeof(float)))
            return value.ToSingle(provider);
        if (ReferenceEquals(targetType, typeof(double)))
            return value.ToDouble(provider);
        if (ReferenceEquals(targetType, typeof(decimal)))
            return value.ToDecimal(provider);
        if (ReferenceEquals(targetType, typeof(DateTime)))
            return value.ToDateTime(provider);
        if (ReferenceEquals(targetType, typeof(string)))
            return value.ToString(provider);
        if (ReferenceEquals(targetType, typeof(object)))
            return (object)value;

        if (ReferenceEquals(targetType, typeof(Enum)))
            return (Enum)value;

        if (ReferenceEquals(targetType, typeof(DBNull)))
            throw new InvalidCastException(); // TODO: Error
        // if (ReferenceEquals(targetType, typeof(Empty))) TODO: WAT?
        //     throw new InvalidCastException();

        throw new InvalidCastException(); // TODO: Error
    }

    #endregion

    #region IComparable<>

    public int CompareTo(Number2 value) => this < value ? -1 : this > value ? 1 : 0;

    #endregion

    #region IEquatable<>

    public bool Equals(Number2 obj) => obj == this;

    #endregion

    #region IRationalNumber<>



    #endregion

    #region IMinMaxValue<>

    static Number2 IMinMaxValue<Number2>.MinValue => MinValue;
    static Number2 IMinMaxValue<Number2>.MaxValue => MaxValue;

    #endregion

    #region Object

    public override bool Equals([NotNullWhen(true)] object? obj) => (obj is Number2 other) && Equals(other);
    public override int GetHashCode() => HashCode.Combine(_dividend, _divisor);

    #endregion

    #region String

    public override string ToString() => throw new NotImplementedException();
    public string ToString(IFormatProvider? provider) => throw new NotImplementedException();
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format) => throw new NotImplementedException();
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? provider) => throw new NotImplementedException();

    public bool TryFormat(Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null) => throw new NotImplementedException();
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null) => throw new NotImplementedException();

    #endregion
}

public interface IRationalNumber<TSelf>
        : IExponentialFunctions<TSelf>,
          IFloatingPointConstants<TSelf>,
          INumber<TSelf>,
          ISignedNumber<TSelf>,
          IHyperbolicFunctions<TSelf>,
          ILogarithmicFunctions<TSelf>,
          IPowerFunctions<TSelf>,
          IRootFunctions<TSelf>,
          ITrigonometricFunctions<TSelf>
        where TSelf : IRationalNumber<TSelf>?
{
    /// <summary>Computes the ceiling of a value.</summary>
    /// <param name="x">The value whose ceiling is to be computed.</param>
    /// <returns>The ceiling of <paramref name="x" />.</returns>
    static virtual TSelf Ceiling(TSelf x) => TSelf.Round(x, MidpointRounding.ToPositiveInfinity);

    /// <summary>Converts a value to a specified integer type using saturation on overflow</summary>
    /// <typeparam name="TInteger">The integer type to which <paramref name="value" /> is converted.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <returns>An instance of <typeparamref name="TInteger" /> created from <paramref name="value" />.</returns>
    static virtual TInteger ConvertToInteger<TInteger>(TSelf value)
        where TInteger : IBinaryInteger<TInteger>
    {
        return TInteger.CreateSaturating(value);
    }

    /// <summary>Converts a value to a specified integer type using platform specific behavior on overflow.</summary>
    /// <typeparam name="TInteger">The integer type to which <paramref name="value" /> is converted.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <returns>An instance of <typeparamref name="TInteger" /> created from <paramref name="value" />.</returns>
    static virtual TInteger ConvertToIntegerNative<TInteger>(TSelf value)
        where TInteger : IBinaryInteger<TInteger>
    {
        return TSelf.ConvertToInteger<TInteger>(value);
    }

    /// <summary>Computes the floor of a value.</summary>
    /// <param name="x">The value whose floor is to be computed.</param>
    /// <returns>The floor of <paramref name="x" />.</returns>
    static virtual TSelf Floor(TSelf x) => TSelf.Round(x, MidpointRounding.ToNegativeInfinity);

    /// <summary>Rounds a value to the nearest integer using the default rounding mode (<see cref="MidpointRounding.AwayFromZero" />).</summary>
    /// <param name="x">The value to round.</param>
    /// <returns>The result of rounding <paramref name="x" /> to the nearest integer using the default rounding mode.</returns>
    static virtual TSelf Round(TSelf x) => TSelf.Round(x, MidpointRounding.AwayFromZero);

    /// <summary>Rounds a value to the nearest integer using the specified rounding mode.</summary>
    /// <param name="x">The value to round.</param>
    /// <param name="mode">The mode under which <paramref name="x" /> should be rounded.</param>
    /// <returns>The result of rounding <paramref name="x" /> to the nearest integer using <paramref name="mode" />.</returns>
    static abstract TSelf Round(TSelf x, MidpointRounding mode);

    /// <summary>Truncates a value.</summary>
    /// <param name="x">The value to truncate.</param>
    /// <returns>The truncation of <paramref name="x" />.</returns>
    static virtual TSelf Truncate(TSelf x) => TSelf.Round(x, MidpointRounding.ToZero);
}
