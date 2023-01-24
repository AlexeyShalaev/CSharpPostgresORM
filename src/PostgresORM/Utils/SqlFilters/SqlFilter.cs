namespace PostgresORM.Utils.SqlFilters;

public class SqlFilter
{
    private string _condition;

    internal SqlFilter(string condition)
    {
        _condition = condition;
    }

    public static SqlFilter operator &(SqlFilter filter1, SqlFilter filter2)
    {
        return new SqlFilter($"({filter1._condition} AND {filter2._condition})");
    }

    public static SqlFilter operator |(SqlFilter filter1, SqlFilter filter2)
    {
        return new SqlFilter($"({filter1._condition} OR {filter2._condition})");
    }

    public override string ToString()
    {
        return _condition;
    }
}