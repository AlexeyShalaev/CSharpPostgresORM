namespace PostgresORM.SqlTypes.String;

public class Json<T> : ISqlType<T>
{
    public T Value { get; set; }

    public static readonly string SqlTypeName = "JSON";

    public Json(T value)
    {
        Value = value;
    }

    public static implicit operator Json<T>(T value)
    {
        return new Json<T>(value);
    }
}