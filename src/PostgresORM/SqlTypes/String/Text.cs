namespace PostgresORM.SqlTypes.String;

public class Text : ISqlType<string>
{
    // Fields and Properties
    public string Value { get; set; }

    public static readonly string SqlTypeName = "TEXT";

    // Castings

    public static implicit operator Text(string value)
    {
        return new Text { Value = value };
    }

    public static implicit operator string(Text text)
    {
        return text.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value;
    }

    public bool Equals(Text rhs)
    {
        return Value == rhs.Value;
    }

    public int CompareTo(Text rhs)
    {
        return Value.CompareTo(rhs.Value);
    }

    // Comparing

    public static bool operator >(Text lhs, Text rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) > 0;
    }

    public static bool operator <(Text lhs, Text rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) < 0;
    }

    public static bool operator >=(Text lhs, Text rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) >= 0;
    }

    public static bool operator <=(Text lhs, Text rhs)
    {
        return lhs.Value.CompareTo(rhs.Value) <= 0;
    }

    public static bool operator ==(Text lhs, Text rhs)
    {
        return lhs.Value == rhs.Value;
    }

    public static bool operator !=(Text lhs, Text rhs)
    {
        return lhs.Value != rhs.Value;
    }
    
    // Binary

    public static Text operator +(Text lhs, Text rhs) => lhs.Value + rhs.Value;
    
}
