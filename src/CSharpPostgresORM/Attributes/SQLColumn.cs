namespace CSharpPostgresORM;

public class SQLColumn : Attribute
{
    public string Flag { get; set; }

    public SQLColumn(string flag)
    {
        Flag = flag;
    }
}