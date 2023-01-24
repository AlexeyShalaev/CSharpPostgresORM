namespace PostgresORM.SqlTypes.Numeric;

public class BigInteger : ISqlType<long>
{
    public long Value { get; set; }

    public static readonly string SqlTypeName = "BIGINT";

    public static implicit operator BigInteger(long value)
    {
        return new BigInteger { Value = value };
    }
}