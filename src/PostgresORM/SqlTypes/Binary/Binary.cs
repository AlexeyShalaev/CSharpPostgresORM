namespace PostgresORM.SqlTypes.Binary;

public class Binary : ISqlType<byte[]>
{
    // Fields and Properties

    public byte[] Value { get; set; }

    public const string SqlTypeName = "BINARY";

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
        return string.Join(" ", Value.Select(x => x.ToString()));
    }

    public bool Equals(Binary rhs)
    {
        return Value == rhs.Value;
    }

    // Comparing
    public static bool operator ==(Binary lhs, Binary rhs)
    {
        return lhs.Value.Length == rhs.Value.Length && lhs.Value.Zip(rhs.Value, (b, b1) => (b, b1)).All(x => x.b == x.b1);
    }

    public static bool operator !=(Binary lhs, Binary rhs)
    {
        return lhs.Value.Length != rhs.Value.Length || lhs.Value.Zip(rhs.Value, (b, b1) => (b, b1)).Any(x => x.b != x.b1);
    }

    // Unary
    public static Binary operator ~(Binary binary) => binary.Value.Select(x => (byte)~x).ToArray();

    // Binary
    public static Binary operator &(Binary lhs, Binary rhs) =>
        lhs.Value.Zip(rhs.Value, (l, r) => (byte)(l & r)).ToArray();

    public static Binary operator |(Binary lhs, Binary rhs) =>
        lhs.Value.Zip(rhs.Value, (l, r) => (byte)(l | r)).ToArray();

    public static Binary operator ^(Binary lhs, Binary rhs) =>
        lhs.Value.Zip(rhs.Value, (l, r) => (byte)(l ^ r)).ToArray();
}