namespace PostgresORM.SqlTypes.Numeric;

public class Integer : ISqlType<int>
{
    // Fields and Properties

    public int Value { get; set; }

    public const string SqlTypeName = "INTEGER";

    // Castings

    public static implicit operator Integer(int value)
        => new Integer { Value = value };

    public static implicit operator int(Integer integer)
        => integer.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(Integer rhs) => Value == rhs.Value;

    public int CompareTo(Integer rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(Integer lhs, Integer rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(Integer lhs, Integer rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(Integer lhs, Integer rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(Integer lhs, Integer rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(Integer lhs, Integer rhs)
        => lhs.Value == rhs.Value;

    public static bool operator !=(Integer lhs, Integer rhs)
        => lhs.Value != rhs.Value;

    // Unary

    public static Integer operator ++(Integer integer) => ++integer.Value;

    public static Integer operator --(Integer integer) => --integer.Value;

    public static Integer operator +(Integer integer) => integer.Value;

    public static Integer operator -(Integer integer) => -integer.Value;

    // Binary

    public static Integer operator +(Integer lhs, Integer rhs)
        => lhs.Value + rhs.Value;

    public static Integer operator -(Integer lhs, Integer rhs)
        => lhs.Value - rhs.Value;

    public static Integer operator *(Integer lhs, Integer rhs)
        => lhs.Value * rhs.Value;

    public static Integer operator /(Integer lhs, Integer rhs)
        => lhs.Value / rhs.Value;

    public static Integer operator %(Integer lhs, Integer rhs)
        => lhs.Value % rhs.Value;
}