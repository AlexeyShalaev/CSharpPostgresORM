namespace PostgresORM.SqlTypes.Numeric;

public class BigInteger : ISqlType<long>
{
    // Fields and Properties

    public long Value { get; set; }

    public const string SqlTypeName = "BIGINT";

    // Castings

    public static implicit operator BigInteger(long value)
        => new BigInteger { Value = value };

    public static implicit operator long(BigInteger bigInteger)
        => bigInteger.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(BigInteger rhs) => Value == rhs.Value;

    public int CompareTo(BigInteger rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(BigInteger lhs, BigInteger rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(BigInteger lhs, BigInteger rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(BigInteger lhs, BigInteger rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(BigInteger lhs, BigInteger rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(BigInteger lhs, BigInteger rhs)
        => lhs.Value == rhs.Value;

    public static bool operator !=(BigInteger lhs, BigInteger rhs)
        => lhs.Value != rhs.Value;

    // Unary

    public static BigInteger operator ++(BigInteger bigInteger) => ++bigInteger.Value;

    public static BigInteger operator --(BigInteger bigInteger) => --bigInteger.Value;

    public static BigInteger operator +(BigInteger bigInteger) => bigInteger.Value;

    public static BigInteger operator -(BigInteger bigInteger) => -bigInteger.Value;

    // Binary

    public static BigInteger operator +(BigInteger lhs, BigInteger rhs)
        => lhs.Value + rhs.Value;

    public static BigInteger operator -(BigInteger lhs, BigInteger rhs)
        => lhs.Value - rhs.Value;

    public static BigInteger operator *(BigInteger lhs, BigInteger rhs)
        => lhs.Value * rhs.Value;

    public static BigInteger operator /(BigInteger lhs, BigInteger rhs)
        => lhs.Value / rhs.Value;

    public static BigInteger operator %(BigInteger lhs, BigInteger rhs)
        => lhs.Value % rhs.Value;
}