namespace PostgresORM.SqlTypes.Numeric;

public class BigSerial : ISqlType<ulong>
{
    // Fields and Properties

    public ulong Value { get; set; }

    public const string SqlTypeName = "BIGSERIAL";

    // Castings

    public static implicit operator BigSerial(ulong value)
        => new BigSerial { Value = value };

    public static implicit operator ulong(BigSerial bigSerial)
        => bigSerial.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(BigSerial rhs) => Value == rhs.Value;

    public int CompareTo(BigSerial rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(BigSerial lhs, BigSerial rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(BigSerial lhs, BigSerial rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(BigSerial lhs, BigSerial rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(BigSerial lhs, BigSerial rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(BigSerial lhs, BigSerial rhs)
       => lhs.Value == rhs.Value;

    public static bool operator !=(BigSerial lhs, BigSerial rhs)
       => lhs.Value != rhs.Value;

    // Unary

    public static BigSerial operator ++(BigSerial bigSerial) => bigSerial.Value++;
    public static BigSerial operator --(BigSerial bigSerial) => bigSerial.Value--;
    public static BigSerial operator +(BigSerial bigSerial) => bigSerial.Value;

    // Binary

    public static BigSerial operator +(BigSerial lhs, BigSerial rhs)
        => lhs.Value + rhs.Value;

    public static BigSerial operator -(BigSerial lhs, BigSerial rhs)
        => lhs.Value - rhs.Value;

    public static BigSerial operator *(BigSerial lhs, BigSerial rhs)
        => lhs.Value * rhs.Value;

    public static BigSerial operator /(BigSerial lhs, BigSerial rhs)
        => lhs.Value / rhs.Value;

    public static BigSerial operator %(BigSerial lhs, BigSerial rhs)
        => lhs.Value % rhs.Value;
}