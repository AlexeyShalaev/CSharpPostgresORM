namespace PostgresORM.SqlTypes.Numeric;

public class Numeric : ISqlType<decimal>
{
    // Fields and Properties

    public decimal Value { get; set; }

    public const string SqlTypeName = "NUMERIC";

    // Castings

    public static implicit operator Numeric(decimal value)
        => new Numeric { Value = value };

    public static implicit operator decimal(Numeric numeric)
        => numeric.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(Numeric rhs) => Value == rhs.Value;

    public int CompareTo(Numeric rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(Numeric lhs, Numeric rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(Numeric lhs, Numeric rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(Numeric lhs, Numeric rhs) 
        => lhs.Value >= rhs.Value;

    public static bool operator <=(Numeric lhs, Numeric rhs) 
        => lhs.Value <= rhs.Value;

    public static bool operator ==(Numeric lhs, Numeric rhs)
        => lhs.Value == rhs.Value;

    public static bool operator !=(Numeric lhs, Numeric rhs)
        => lhs.Value != rhs.Value;

    // Unary

    public static Numeric operator ++(Numeric numeric) => ++numeric.Value;

    public static Numeric operator --(Numeric numeric) => --numeric.Value;

    public static Numeric operator +(Numeric numeric) => numeric.Value;

    public static Numeric operator -(Numeric numeric) => -numeric.Value;

    // Binary

    public static Numeric operator +(Numeric lhs, Numeric rhs)
        => lhs.Value + rhs.Value;

    public static Numeric operator -(Numeric lhs, Numeric rhs)
        => lhs.Value - rhs.Value;

    public static Numeric operator *(Numeric lhs, Numeric rhs)
        => lhs.Value * rhs.Value;

    public static Numeric operator /(Numeric lhs, Numeric rhs)
        => lhs.Value / rhs.Value;

    public static Numeric operator %(Numeric lhs, Numeric rhs)
        => lhs.Value % rhs.Value;
}