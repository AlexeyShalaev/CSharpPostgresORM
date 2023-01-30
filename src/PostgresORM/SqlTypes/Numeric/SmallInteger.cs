namespace PostgresORM.SqlTypes.Numeric;

public class SmallInteger : ISqlType<short>
{
    // Fields and Properties

    public short Value { get; set; }

    public const string SqlTypeName = "SMALLINT";

    // Castings

    public static implicit operator SmallInteger(short value)
        => new SmallInteger { Value = value };

    public static implicit operator short(SmallInteger shortNumber)
        => shortNumber.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(SmallInteger rhs) => Value == rhs.Value;

    public int CompareTo(SmallInteger rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(SmallInteger lhs, SmallInteger rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(SmallInteger lhs, SmallInteger rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(SmallInteger lhs, SmallInteger rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(SmallInteger lhs, SmallInteger rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(SmallInteger lhs, SmallInteger rhs) 
        => lhs.Value == rhs.Value;

    public static bool operator !=(SmallInteger lhs, SmallInteger rhs) 
        => lhs.Value != rhs.Value;

    // Unary

    public static SmallInteger operator ++(SmallInteger shortNumber) => shortNumber.Value++;

    public static SmallInteger operator --(SmallInteger shortNumber) => shortNumber.Value--;

    public static SmallInteger operator +(SmallInteger shortNumber) => shortNumber.Value;

    // Binary

    public static SmallInteger operator +(SmallInteger lhs, SmallInteger rhs) 
        => (SmallInteger)(lhs.Value + rhs.Value);

    public static SmallInteger operator -(SmallInteger lhs, SmallInteger rhs) 
        => (SmallInteger)(lhs.Value - rhs.Value);

    public static SmallInteger operator *(SmallInteger lhs, SmallInteger rhs) 
        => (SmallInteger)(lhs.Value * rhs.Value);

    public static SmallInteger operator /(SmallInteger lhs, SmallInteger rhs) 
        => (SmallInteger)(lhs.Value / rhs.Value);

    public static SmallInteger operator %(SmallInteger lhs, SmallInteger rhs)
        => (SmallInteger)(lhs.Value % rhs.Value);
}