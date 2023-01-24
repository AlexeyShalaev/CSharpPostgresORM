namespace PostgresORM.SqlTypes.Numeric;

public class BigSerial : ISqlType<long>
{
    public long Value { get; set; }

    public static readonly string SqlTypeName = "BIGSERIAL";

    public static implicit operator BigSerial(long value)
    {
        return new BigSerial { Value = value };
    }
}