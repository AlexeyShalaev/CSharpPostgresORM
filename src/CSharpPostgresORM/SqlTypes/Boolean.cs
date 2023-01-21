namespace DefaultNamespace;

public class Boolean : ISqlType<bool>
{
    public bool Value { get; set; }

    public static string SqlTypeName = "BOOLEAN";

    public static implicit operator Boolean(bool value)
    {
        return new Boolean { Value = value };
    }
}