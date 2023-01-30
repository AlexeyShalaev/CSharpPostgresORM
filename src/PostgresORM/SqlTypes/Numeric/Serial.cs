namespace PostgresORM.SqlTypes.Numeric;

public class Serial : ISqlType<uint>
{
    // Fields and Properties

    public uint Value { get; set; }

    public const string SqlTypeName = "SERIAL";

    // Castings

    public static implicit operator Serial(uint value)
        => new Serial { Value = value };

    public static implicit operator uint(Serial serial)
        => serial.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(Serial rhs) => Value == rhs.Value;

    public int CompareTo(Serial rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(Serial lhs, Serial rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(Serial lhs, Serial rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(Serial lhs, Serial rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(Serial lhs, Serial rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(Serial lhs, Serial rhs)
       => lhs.Value == rhs.Value;

    public static bool operator !=(Serial lhs, Serial rhs)
       => lhs.Value != rhs.Value;

    // Unary

    public static Serial operator ++(Serial serial) => serial.Value++;
    public static Serial operator --(Serial serial) => serial.Value--;
    public static Serial operator +(Serial serial) => serial.Value;

    // Binary

    public static Serial operator +(Serial lhs, Serial rhs)
        => lhs.Value + rhs.Value;

    public static Serial operator -(Serial lhs, Serial rhs)
        => lhs.Value - rhs.Value;

    public static Serial operator *(Serial lhs, Serial rhs)
        => lhs.Value * rhs.Value;

    public static Serial operator /(Serial lhs, Serial rhs)
        => lhs.Value / rhs.Value;

    public static Serial operator %(Serial lhs, Serial rhs)
        => lhs.Value % rhs.Value;
}