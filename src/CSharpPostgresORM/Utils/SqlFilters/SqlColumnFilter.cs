namespace CSharpPostgresORM.Utils.SqlFilters;

public class SqlColumnFilter
{
    public SqlColumnFilter(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static SqlFilter operator <(SqlColumnFilter columnFilter, object val)
    {
        return new SqlFilter($"{columnFilter.Name} < '{val}'");
    }

    public static SqlFilter operator >(SqlColumnFilter columnFilter, object val)
    {
        return new SqlFilter($"{columnFilter.Name} > '{val}'");
    }

    public static SqlFilter operator ==(SqlColumnFilter columnFilter, object val)
    {
        return new SqlFilter($"{columnFilter.Name} = '{val}'");
    }

    public static SqlFilter operator !=(SqlColumnFilter columnFilter, object val)
    {
        return new SqlFilter($"{columnFilter.Name} != '{val}'");
    }

    public SqlFilter Contains(object val)
    {
        return new SqlFilter($"{Name} LIKE '%{val}%'");
    }
    
    public SqlFilter StartsWith(object val)
    {
        return new SqlFilter($"{Name} LIKE '{val}%'");
    }
    
    public SqlFilter FinishesWith(object val)
    {
        return new SqlFilter($"{Name} LIKE '%{val}'");
    }
}