namespace CSharpPostgresORM.SqlTypes
{
    public class BigSerial : ISqlType<ulong>
    {
        public ulong Value { get; set; }

        public static readonly string SqlTypeName = "BIGSERIAL";

        public static implicit operator BigSerial(ulong value)
        {
            return new BigSerial { Value = value };
        }
    }
}