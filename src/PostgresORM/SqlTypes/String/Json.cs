using System.Text.Json;

namespace PostgresORM.SqlTypes.String;

public class Json<T> : ISqlType<T>
{
    // Fields and Properties
    public T Value { get; set; }

    public static readonly string SqlTypeName = "JSONB";

    public Json(T value)
    {
        Value = value;
    }
    
    // Castings

    public static implicit operator Json<T>(T value)
    {
        return new Json<T>(value);
    }

    public static implicit operator Json<T?>(string value)
    {
        return new Json<T?>(JsonSerializer.Deserialize<T>(value));
    }
    
    public static implicit operator T(Json<T> json)
    {
        return json.Value;
    }

    // Other overloads

    public override string ToString()
    {
        return Value.ToString();
    }
}