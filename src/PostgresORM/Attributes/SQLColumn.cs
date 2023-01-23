namespace PostgresORM.Attributes;

public sealed class SqlColumn : Attribute
{
    public string Flag { get; set; }

    public SqlColumn(string flag)
    {
        Flag = flag;
    }
}