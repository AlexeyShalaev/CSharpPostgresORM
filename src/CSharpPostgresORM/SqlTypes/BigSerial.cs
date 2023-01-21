namespace CSharpPostgresORM;

public class BigSerial : ISqlType<int>
{
    public int Value { get; set; }

    public static string SqlTypeName = "BIGSERIAL";

    public static implicit operator BigSerial(int value)
    {
        return new BigSerial { Value = value };
    }

    /* public static implicit operator BigSerial(short value)
     {
         return new BigSerial { Value = value };
     }
     */
}