using System.Text.Json;

namespace PostgresORM.SqlTypes.String;

public class Json<T> : ISqlType<T>
{
    public T Value { get; set; }

    public static readonly string SqlTypeName = "JSONB";

    public Json(T value)
    {
        Value = value;
    }

    public static implicit operator Json<T>(T value)
    {
        return new Json<T>(value);
    }

    public static implicit operator Json<T?>(string value)
    {
        return new Json<T?>(JsonSerializer.Deserialize<T>(value));
    }
}