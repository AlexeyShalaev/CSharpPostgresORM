namespace PostgresORM.SqlTypes.Numeric;

public class Serial : ISqlType<uint>
{
    public uint Value { get; set; }

    public static readonly string SqlTypeName = "SERIAL";

    public static implicit operator Serial(uint value)
    {
        return new Serial { Value = value };
    }
}