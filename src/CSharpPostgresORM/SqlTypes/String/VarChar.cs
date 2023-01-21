namespace CSharpPostgresORM.SqlTypes
{
    public class VarChar : ISqlType<string>
    {
        public string Value { get; set; }

        public static readonly string SqlTypeName = "VARCHAR";

        public static implicit operator VarChar(string value)
        {
            return new VarChar { Value = value };
        }
    }
}