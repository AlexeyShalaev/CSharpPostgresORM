namespace PostgresORM.SqlTypes.String;

public class Character : ISqlType<char>
{
    public char Value { get; set; }

    public static readonly string SqlTypeName = "CHARACTER";

    public static implicit operator Character(char value)
    {
        return new Character { Value = value };
    }
}