namespace PostgresORM.Attributes;

public sealed class ColumnLength : Attribute
{
    public int Length { get; set; }

    public ColumnLength(int length)
    {
        Length = length;
    }
}