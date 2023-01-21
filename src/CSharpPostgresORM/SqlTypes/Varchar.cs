namespace DefaultNamespace;

public class Varchar : ISqlType<string>
{
    public string Value { get; set; }

    public static string SqlTypeName = "VARCHAR";

    public static implicit operator Varchar(string value)
    {
        return new Varchar { Value = value };
    }
}