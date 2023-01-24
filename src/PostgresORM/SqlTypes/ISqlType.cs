namespace PostgresORM.SqlTypes;

public interface ISqlType
{
    public static string? SqlTypeName;
}

public interface ISqlType<T> : ISqlType
{
    public T Value { get; set; }
}