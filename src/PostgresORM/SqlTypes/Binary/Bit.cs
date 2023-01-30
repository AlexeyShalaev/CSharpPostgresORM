namespace PostgresORM.SqlTypes.Binary;

public class Bit : ISqlType<bool>
{
    // Fields and Properties
    
    public bool Value { get; set; }

    public const string SqlTypeName = "BIT";

    // Castings

    public static implicit operator Bit(bool value)
    {
        return new Bit { Value = value };
    }

    public static implicit operator bool(Bit bit)
    {
        return bit.Value;
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

    public static bool operator ==(Bit lhs, Bit rhs)
    {
        return lhs.Value == rhs.Value;
    }

    public static bool operator !=(Bit lhs, Bit rhs)
    {
        return lhs.Value != rhs.Value;
    }

    // Unary

    public static Bit operator ~(Bit bit) => !bit.Value;

    // Binary

    public static Bit operator &(Bit lhs, Bit rhs) => lhs.Value & rhs.Value;

    public static Bit operator |(Bit lhs, Bit rhs) => lhs.Value | rhs.Value;

    public static Bit operator ^(Bit lhs, Bit rhs) => lhs.Value ^ rhs.Value;
}