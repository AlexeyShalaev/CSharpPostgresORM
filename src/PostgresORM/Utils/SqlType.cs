using System.Reflection;
using System.Text.RegularExpressions;
using PostgresORM.SqlTypes;
using PostgresORM.SqlTypes.Binary;
using PostgresORM.SqlTypes.Numeric;
using PostgresORM.SqlTypes.String;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;
using Decimal = PostgresORM.SqlTypes.Numeric.Decimal;

namespace PostgresORM.Utils;

public static class SqlType
{
    //Public Methods
    public static bool IsISqlType(PropertyInfo propertyInfo)
    {
        return typeof(ISqlType).IsAssignableFrom(propertyInfo.PropertyType);
    }

    public static string CreateSqlTypeTemplate(string className, string typeName, bool fileMode = false)
    {
        var template = $@"
                public class {className} : ISqlType<{typeName}>
                {{
                    // Fields and Properties
                    public {typeName} Value {{ get; set; }}

                    public static readonly string SqlTypeName = ""{typeName.ToUpper()}"";

                    // Castings

                    public static implicit operator {className}({typeName} value)
                    {{
                        return new {className} {{ Value = value }};
                    }}

                    public static implicit operator {typeName}({className} {className.ToLower()})
                    {{
                        return {className.ToLower()}.Value;
                    }}

                    // Other overloads

                    public override string ToString()
                    {{
                        return Value.ToString();
                    }}

                    public bool Equals({className} rhs)
                    {{
                        return Value == rhs.Value;
                    }}

                    public int CompareTo({className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    // Comparing

                    public static bool operator >({className} lhs, {className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    public static bool operator <({className} lhs, {className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    public static bool operator >=({className} lhs, {className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    public static bool operator <=({className} lhs, {className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    public static bool operator ==({className} lhs, {className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    public static bool operator !=({className} lhs, {className} rhs)
                    {{
                        throw new NotImplementedException();
                    }}

                    // Unary

                    public static {className} operator ++({className} {className.ToLower()}) => throw new NotImplementedException();

                    public static {className} operator --({className} {className.ToLower()}) => throw new NotImplementedException();

                    public static {className} operator +({className} {className.ToLower()}) => throw new NotImplementedException();

                    public static {className} operator -({className} {className.ToLower()}) => throw new NotImplementedException();

                    // Binary

                    public static {className} operator +({className} lhs, {className} rhs) => throw new NotImplementedException();

                    public static {className} operator -({className} lhs, {className} rhs) => throw new NotImplementedException();

                    public static {className} operator *({className} lhs, {className} rhs) => throw new NotImplementedException();

                    public static {className} operator /({className} lhs, {className} rhs) => throw new NotImplementedException();

                    public static {className} operator %({className} lhs, {className} rhs) => throw new NotImplementedException();
                }}    
        ";
        if (fileMode)
        {
            File.WriteAllText($"{className}.cs", template);
        }

        return template;
    }

    //Internal Methods

    internal static bool IsConvertableSqlType(PropertyInfo propertyInfo)
    {
        if (!propertyInfo.PropertyType.IsTypeDefinition)
        {
            var typeName = propertyInfo.PropertyType.FullName;
            var pattern = @"[a-zA-Z0-9.]+`";
            var match = Regex.Match(typeName, pattern);
            if (match.Success)
            {
                typeName = match.Value.Remove(match.Value.Length - 1);
                if (typeName is "System.Nullable")
                {
                    return true;
                }
            }

            return false;
        }

        var typeCode = GetTypeCode(propertyInfo.PropertyType);

        switch (typeCode)
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Boolean:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
            case TypeCode.String:
                return true;
        }

        return false;
    }

    internal static bool IsSqlType(PropertyInfo propertyInfo)
    {
        return IsISqlType(propertyInfo) || IsConvertableSqlType(propertyInfo);
    }

    internal static string GetSqlTypeName(Type type)
    {
        var typeCode = GetTypeCode(type);

        switch (typeCode)
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
                return Binary.SqlTypeName;
            case TypeCode.Boolean:
                return Boolean.SqlTypeName;
            case TypeCode.UInt16:
                return Integer.SqlTypeName;
            case TypeCode.UInt32:
                return BigInteger.SqlTypeName;
            case TypeCode.Int16:
                return SmallInteger.SqlTypeName;
            case TypeCode.Int32:
                return Integer.SqlTypeName;
            case TypeCode.Int64:
                return BigInteger.SqlTypeName;
            case TypeCode.Decimal:
            case TypeCode.Double:
                return Decimal.SqlTypeName;
            case TypeCode.Single:
                return Real.SqlTypeName;
            case TypeCode.String:
                return VarChar.SqlTypeName;
        }

        throw new PostgresOrmException($"Unknown SQL Type: {type}");
    }

    //Private Methods

    private static TypeCode GetTypeCode(Type type)
    {
        if (!type.IsTypeDefinition)
        {
            var typeName = type.FullName;
            var pattern = @"\[\[[a-zA-Z.0-9]+,";
            var match = Regex.Match(typeName, pattern);
            if (match.Success)
            {
                typeName = match.Value.Remove(match.Value.Length - 1).Remove(0, 2);
                return Type.GetTypeCode(Type.GetType(typeName));
            }
        }

        return Type.GetTypeCode(type);
    }
}