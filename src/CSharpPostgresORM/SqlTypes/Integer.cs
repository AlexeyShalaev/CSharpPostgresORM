namespace DefaultNamespace;

public class Integer : ISqlType<int>
{
    public int Value { get; set; }

    public static string SqlTypeName = "INTEGER";

    public static implicit operator Integer(int value)
    {
        return new Integer { Value = value };
    }
}