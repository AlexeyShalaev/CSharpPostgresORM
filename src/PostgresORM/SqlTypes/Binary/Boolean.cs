namespace PostgresORM.SqlTypes.Binary;

public class Boolean : ISqlType<bool>
{
    public bool Value { get; set; }

    public const string SqlTypeName = "BOOLEAN";

    // Castings

    public static implicit operator Boolean(bool value)
    {
        return new Boolean { Value = value };
    }

    public static implicit operator bool(Boolean boolean)
    {
        return boolean.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(Bit rhs)
    {
        return Value == rhs.Value;
    }

    // Comparing

    public static bool operator ==(Boolean lhs, Boolean rhs)
    {
        return lhs.Value == rhs.Value;
    }

    public static bool operator !=(Boolean lhs, Boolean rhs)
    {
        return lhs.Value != rhs.Value;
    }

    // Unary

    public static Bit operator ~(Boolean bit) => !bit.Value;

    // Binary

    public static Boolean operator &(Boolean lhs, Boolean rhs) => lhs.Value & rhs.Value;

    public static Boolean operator |(Boolean lhs, Boolean rhs) => lhs.Value | rhs.Value;

    public static Boolean operator ^(Boolean lhs, Boolean rhs) => lhs.Value ^ rhs.Value;
}