namespace PostgresORM.SqlTypes.Numeric;

public class Integer : ISqlType<int>
{
    // Fields and Properties
    public int Value { get; set; }

    public static readonly string SqlTypeName = "INTEGER";

    // Castings

    public static implicit operator Integer(int value)
    {
        return new Integer { Value = value };
    }

    public static implicit operator int(Integer integer)
    {
        return integer.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(Integer rhs)
    {
        return Value == rhs.Value;
    }

    public int CompareTo(Integer rhs)
    {
        throw new NotImplementedException();
    }

    // Comparing

    public static bool operator >(Integer lhs, Integer rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator <(Integer lhs, Integer rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator >=(Integer lhs, Integer rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator <=(Integer lhs, Integer rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Integer lhs, Integer rhs)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Integer lhs, Integer rhs)
    {
        throw new NotImplementedException();
    }

    // Unary

    public static Integer operator ++(Integer integer) => throw new NotImplementedException();

    public static Integer operator --(Integer integer) => throw new NotImplementedException();

    public static Integer operator +(Integer integer) => throw new NotImplementedException();

    public static Integer operator -(Integer integer) => throw new NotImplementedException();

    // Binary

    public static Integer operator +(Integer lhs, Integer rhs) => throw new NotImplementedException();

    public static Integer operator -(Integer lhs, Integer rhs) => throw new NotImplementedException();

    public static Integer operator *(Integer lhs, Integer rhs) => throw new NotImplementedException();

    public static Integer operator /(Integer lhs, Integer rhs) => throw new NotImplementedException();

    public static Integer operator %(Integer lhs, Integer rhs) => throw new NotImplementedException();
}