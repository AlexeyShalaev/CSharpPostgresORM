namespace CSharpPostgresORM.Attributes;

internal sealed class SqlColumn : Attribute
{
    public string Flag { get; set; }

    public SqlColumn(string flag)
    {
        Flag = flag;
    }
}