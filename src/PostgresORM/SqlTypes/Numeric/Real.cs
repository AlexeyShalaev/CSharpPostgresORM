namespace PostgresORM.SqlTypes.Numeric;

public class Real : ISqlType<float>
{
    // Fields and Properties

    public float Value { get; set; }

    public const string SqlTypeName = "REAL";

    // Castings

    public static implicit operator Real(float value)
        => new Real { Value = value };

    public static implicit operator float(Real floatNumber)
        => floatNumber.Value;

    // Other overloads

    public override string ToString() => Value.ToString();

    public bool Equals(Real rhs) => Value == rhs.Value;

    public int CompareTo(Real rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(Real lhs, Real rhs)
        => lhs.Value > rhs.Value;

    public static bool operator <(Real lhs, Real rhs)
        => lhs.Value < rhs.Value;

    public static bool operator >=(Real lhs, Real rhs)
        => lhs.Value >= rhs.Value;

    public static bool operator <=(Real lhs, Real rhs)
        => lhs.Value <= rhs.Value;

    public static bool operator ==(Real lhs, Real rhs)
        => lhs.Value == rhs.Value;

    public static bool operator !=(Real lhs, Real rhs)
        => lhs.Value != rhs.Value;

    // Unary

    public static Real operator ++(Real floatNumber) => floatNumber.Value++;

    public static Real operator --(Real floatNumber) => floatNumber.Value--;

    public static Real operator +(Real floatNumber) => floatNumber.Value;

    public static Real operator -(Real floatNumber) => -floatNumber.Value;

    // Binary

    public static Real operator +(Real lhs, Real rhs)
        => lhs.Value + rhs.Value;

    public static Real operator -(Real lhs, Real rhs)
        => lhs.Value - rhs.Value;

    public static Real operator *(Real lhs, Real rhs)
        => lhs.Value * rhs.Value;

    public static Real operator /(Real lhs, Real rhs)
        => lhs.Value / rhs.Value;

    public static Real operator %(Real lhs, Real rhs)
        => lhs.Value % rhs.Value;
}