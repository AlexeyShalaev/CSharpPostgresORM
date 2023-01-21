namespace CSharpPostgresORM.SqlTypes.Binary
{
    public class Bit : ISqlType<bool>
    {
        public bool Value { get; set; }

        public static readonly string SqlTypeName = "BIT";

        public static implicit operator Bit(bool value)
        {
            return new Bit { Value = value };
        }
    }
}
