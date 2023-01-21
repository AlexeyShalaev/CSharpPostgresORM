namespace CSharpPostgresORM.SqlTypes.Numeric
{
    public class SmallInteger : ISqlType<short>
    {
        public short Value { get; set; }

        public static readonly string SqlTypeName = "SMALLINT";

        public static implicit operator SmallInteger(short value)
        {
            return new SmallInteger { Value = value };
        }
    }
}
