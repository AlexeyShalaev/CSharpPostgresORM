namespace PostgresORM.SqlTypes.Numeric;

public class Decimal : ISqlType<decimal>
{
    public decimal Value { get; set; }

    public static readonly string SqlTypeName = "DECIMAL";

    public static implicit operator Decimal(decimal value)
    {
        return new Decimal { Value = value };
    }
}