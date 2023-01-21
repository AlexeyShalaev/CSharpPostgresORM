namespace CSharpPostgresORM.SqlTypes
{
    public class Real : ISqlType<Single>
    {
        public Single Value { get; set; }

        public static readonly string SqlTypeName = "REAL";

        public static implicit operator Real(float value)
        {
            return new Real { Value = value };
        }
    }
}
