using System.Reflection;

namespace CSharpPostgresORM.Utils;

internal static class SqlType
{
    public static bool IsISqlType(PropertyInfo propertyInfo)
    {
        return typeof(ISqlType).IsAssignableFrom(propertyInfo.PropertyType);
    }

    public static string ToSqlType(Type type)
    {
        var typeCode = Type.GetTypeCode(type);

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
                return Integer.SqlTypeName;
            case TypeCode.Boolean:
                return Boolean.SqlTypeName;
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return BigSerial.SqlTypeName;
            case TypeCode.String:
                return Varchar.SqlTypeName;
        }

        throw new PostgresOrmException("Unknown SQL Type");
    }
}