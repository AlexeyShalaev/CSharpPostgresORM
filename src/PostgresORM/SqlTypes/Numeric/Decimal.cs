namespace PostgresORM.SqlTypes.Numeric;

public class Decimal : ISqlType<decimal>
{
    // Fields and Properties

    public decimal Value { get; set; }

    public const string SqlTypeName = "DECIMAL";

    // Castings

    public static implicit operator Decimal(decimal value)
        => new Decimal { Value = value };

    public static implicit operator decimal(Decimal decimalNumber)
        => decimalNumber.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(Decimal rhs) => Value == rhs.Value;

    public int CompareTo(Decimal rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(Decimal lhs, Decimal rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(Decimal lhs, Decimal rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(Decimal lhs, Decimal rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(Decimal lhs, Decimal rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(Decimal lhs, Decimal rhs)
        => lhs.Value == rhs.Value;

    public static bool operator !=(Decimal lhs, Decimal rhs)
        => lhs.Value != rhs.Value;

    // Unary

    public static Decimal operator ++(Decimal decimalNumber) => decimalNumber.Value++;

    public static Decimal operator --(Decimal decimalNumber) => decimalNumber.Value--;

    public static Decimal operator +(Decimal decimalNumber) => decimalNumber.Value;

    public static Decimal operator -(Decimal decimalNumber) => -decimalNumber.Value;

    // Binary

    public static Decimal operator +(Decimal lhs, Decimal rhs) 
        => lhs.Value + rhs.Value;

    public static Decimal operator -(Decimal lhs, Decimal rhs) 
        => lhs.Value - rhs.Value;

    public static Decimal operator *(Decimal lhs, Decimal rhs) 
        => lhs.Value * rhs.Value;

    public static Decimal operator /(Decimal lhs, Decimal rhs) 
        => lhs.Value / rhs.Value;

    public static Decimal operator %(Decimal lhs, Decimal rhs)
        => lhs.Value % rhs.Value;
}