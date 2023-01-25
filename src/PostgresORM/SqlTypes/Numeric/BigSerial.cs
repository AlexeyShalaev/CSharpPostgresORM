namespace PostgresORM.SqlTypes.Numeric;

public class BigSerial : ISqlType<long>
{
    // Fields and Properties
    public long Value { get; set; }

    public static readonly string SqlTypeName = "BIGSERIAL";

    // Castings

    public static implicit operator BigSerial(long value)
    {
        return new BigSerial { Value = value };
    }

    public static implicit operator long(BigSerial bigSerial)
    {
        return bigSerial.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(BigSerial rhs)
    {
        return Value == rhs.Value;
    }

    public int CompareTo(BigSerial rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(BigSerial lhs, BigSerial rhs)
    {
        return lhs.Value > rhs.Value;
    }

    public static bool operator <(BigSerial lhs, BigSerial rhs)
    {
        return lhs.Value < rhs.Value;
    }

    public static bool operator >=(BigSerial lhs, BigSerial rhs)
    {
        return lhs.Value >= rhs.Value;
    }

    public static bool operator <=(BigSerial lhs, BigSerial rhs)
    {
        return lhs.Value <= rhs.Value;
    }

    public static bool operator ==(BigSerial lhs, BigSerial rhs)
    {
        return lhs.Value == rhs.Value;
    }

    public static bool operator !=(BigSerial lhs, BigSerial rhs)
    {
        return lhs.Value != rhs.Value;
    }

    // Unary

    public static BigSerial operator ++(BigSerial bigSerial) => bigSerial.Value + 1;
    public static BigSerial operator --(BigSerial bigSerial) => bigSerial.Value - 1;
    public static BigSerial operator +(BigSerial bigSerial) => bigSerial.Value;
    public static BigSerial operator -(BigSerial bigSerial) => -bigSerial.Value;

    // Binary

    public static BigSerial operator +(BigSerial lhs, BigSerial rhs)
        => lhs.Value + rhs.Value;

    public static BigSerial operator -(BigSerial lhs, BigSerial rhs)
        => lhs.Value - rhs.Value;

    public static BigSerial operator *(BigSerial lhs, BigSerial rhs)
        => lhs.Value * rhs.Value;

    public static BigSerial operator /(BigSerial lhs, BigSerial rhs)
    {
        if (rhs.Value == 0)
        {
            throw new DivideByZeroException();
        }

        return lhs.Value / rhs.Value;
    }

    public static BigSerial operator %(BigSerial lhs, BigSerial rhs)
        => lhs.Value % rhs.Value;
}