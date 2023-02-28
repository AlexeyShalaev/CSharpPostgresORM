using PostgresORM.SqlTypes;

public class Character : ISqlType<char>
{
    // Fields and Properties
    public char Value { get; set; }

    public static readonly string SqlTypeName = "CHAR";

    // Castings

    public static implicit operator Character(char value)
    {
        return new Character { Value = value };
    }

    public static implicit operator char(Character character)
    {
        return character.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(Character rhs)
    {
        return Value == rhs.Value;
    }

    public int CompareTo(Character rhs)
    {
        if (Value < rhs.Value) return -1;
        if (Value > rhs.Value) return 1;
        return 0;
    }

    // Comparing

    public static bool operator >(Character lhs, Character rhs)
    {
        return lhs.Value > rhs.Value;
    }

    public static bool operator <(Character lhs, Character rhs)
    {
        return lhs.Value < rhs.Value;
    }

    public static bool operator >=(Character lhs, Character rhs)
    {
        return lhs.Value >= rhs.Value;
    }

    public static bool operator <=(Character lhs, Character rhs)
    {
        return lhs.Value <= rhs.Value;
    }

    public static bool operator ==(Character lhs, Character rhs)
    {
        return lhs.Value == rhs.Value;
    }

    public static bool operator !=(Character lhs, Character rhs)
    {
        return lhs.Value != rhs.Value;
    }

    // Unary

    public static Character operator ++(Character character) => (char)(character.Value + 1);

    public static Character operator --(Character character) => (char)(character.Value - 1);

    public static Character operator +(Character character) => character.Value;

    public static Character operator -(Character character) => (char)(-character.Value);

    // Binary

    public static Character operator +(Character lhs, Character rhs) => (char)(lhs.Value + rhs.Value);

    public static Character operator -(Character lhs, Character rhs) => (char)(lhs.Value - rhs.Value);

    public static Character operator *(Character lhs, Character rhs) => (char)(lhs.Value * rhs.Value);

    public static Character operator /(Character lhs, Character rhs) => (char)(lhs.Value / rhs.Value);

    public static Character operator %(Character lhs, Character rhs) => (char)(lhs.Value % rhs.Value);
}