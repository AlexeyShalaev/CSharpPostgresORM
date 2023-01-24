namespace PostgresORM.SqlTypes.String;

public class Text : ISqlType<string>
{
    public string Value { get; set; }

    public static readonly string SqlTypeName = "TEXT";

    public static implicit operator Text(string value)
    {
        return new Text { Value = value };
    }
}