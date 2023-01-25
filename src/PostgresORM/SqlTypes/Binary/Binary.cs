namespace PostgresORM.SqlTypes.Binary;

public class Binary : ISqlType<byte[]>
{
    // Fields and Properties
    public byte[] Value { get; set; }

    public static readonly string SqlTypeName = "BINARY";

    // Castings

    public static implicit operator Binary(byte[] value)
    {
        return new Binary { Value = value };
    }

    public static implicit operator byte[](Binary binary)
    {
        return binary.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(Binary rhs)
    {
        return Value == rhs.Value;
    }

    public int CompareTo(Binary rhs)
    {
        throw new NotImplementedException();
    }

    // Comparing

    public static bool operator >(Binary lhs, Binary rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator <(Binary lhs, Binary rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator >=(Binary lhs, Binary rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator <=(Binary lhs, Binary rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Binary lhs, Binary rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Binary lhs, Binary rhs)
    {
        throw new NotImplementedException();
    }

    // Unary

    public static Binary operator ++(Binary binary) => throw new NotImplementedException();

    public static Binary operator --(Binary binary) => throw new NotImplementedException();

    public static Binary operator +(Binary binary) => throw new NotImplementedException();

    public static Binary operator -(Binary binary) => throw new NotImplementedException();

    // Binary

    public static Binary operator +(Binary lhs, Binary rhs) => throw new NotImplementedException();

    public static Binary operator -(Binary lhs, Binary rhs) => throw new NotImplementedException();

    public static Binary operator *(Binary lhs, Binary rhs) => throw new NotImplementedException();

    public static Binary operator /(Binary lhs, Binary rhs) => throw new NotImplementedException();

    public static Binary operator %(Binary lhs, Binary rhs) => throw new NotImplementedException();
}