namespace PostgresORM.SqlTypes.String;

public class VarChar : ISqlType<string>
{
    // Fields and Properties
    public string Value { get; set; }

    public static readonly string SqlTypeName = "VARCHAR";

    // Castings

    public static implicit operator VarChar(string value)
    {
        return new VarChar { Value = value };
    }

    public static implicit operator string(VarChar varchar)
    {
        return varchar.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value;
    }

    public bool Equals(VarChar rhs)
    {
        return Value == rhs.Value;
    }

    public int CompareTo(VarChar rhs)
    {
        return Value.CompareTo(rhs.Value);
    }

    // Comparing

    public static bool operator >(VarChar lhs, VarChar rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) > 0;
    }

    public static bool operator <(VarChar lhs, VarChar rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) < 0;
    }

    public static bool operator >=(VarChar lhs, VarChar rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) >= 0;
    }

    public static bool operator <=(VarChar lhs, VarChar rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) <= 0;
    }

    public static bool operator ==(VarChar lhs, VarChar rhs)
    {
        return lhs.Value == rhs.Value;
    }

    public static bool operator !=(VarChar lhs, VarChar rhs)
    {
        return lhs.Value != rhs.Value;
    }
    
    // Binary

    public static Text operator +(VarChar lhs, VarChar rhs) => lhs.Value + rhs.Value;
    }