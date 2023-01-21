namespace CSharpPostgresORM.SqlTypes
{
    public class Numeric : ISqlType<decimal>
    {
        public decimal Value { get; set; }

        public static readonly string SqlTypeName = "NUMERIC";

        public static implicit operator Numeric(decimal value)
        {
            return new Numeric { Value = value };
        }
    }
}