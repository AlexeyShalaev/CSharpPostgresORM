using System.Text.Json;

namespace PostgresORM.SqlTypes.String;

public class Json<T> : ISqlType<string>
{
    public string Value { get; set; }

    public static readonly string SqlTypeName = "json";

    public static implicit operator Json<T>(string value)
    {
        return new Json<T> { Value = value };
    }
    
    public static implicit operator Json<T>(T value)
    {
        return new Json<T> { Value = JsonSerializer.Serialize(value) };
    }
}