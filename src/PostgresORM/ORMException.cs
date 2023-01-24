using System.Runtime.Serialization;

namespace PostgresORM;

[Serializable]
public class PostgresOrmException : Exception
{
    public PostgresOrmException()
    {
    }

    public PostgresOrmException(string message) : base(message)
    {
    }

    public PostgresOrmException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public PostgresOrmException(string message, Exception exception) : base(message, exception)
    {
    }
}