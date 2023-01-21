namespace CSharpPostgresORM.SqlTypes
{
    public class Binary : ISqlType<byte[]>
    {
        public byte[] Value { get; set; }

        public static readonly string SqlTypeName = "BINARY";

        public static implicit operator Binary(byte[] value)
        {
            return new Binary { Value = value };
        }
    }
}