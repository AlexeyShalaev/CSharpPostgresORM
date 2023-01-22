using System.Reflection;
using CSharpPostgresORM.SqlTypes;
using Boolean = CSharpPostgresORM.SqlTypes.Boolean;
using Decimal = CSharpPostgresORM.SqlTypes.Decimal;

namespace CSharpPostgresORM.Utils;

internal static class SqlType
{
    public static bool IsISqlType(PropertyInfo propertyInfo)
    {
        return typeof(ISqlType).IsAssignableFrom(propertyInfo.PropertyType);
    }


    public static bool IsConvertableSqlType(PropertyInfo propertyInfo)
    {
        var typeCode = Type.GetTypeCode(propertyInfo.PropertyType);

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

    public static bool IsSqlType(PropertyInfo propertyInfo)
    {
        return IsISqlType(propertyInfo) || IsConvertableSqlType(propertyInfo);
    }


    public static string GetSqlTypeName(Type type)
    {
        var typeCode = Type.GetTypeCode(type);

        switch (typeCode)
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
                return Binary.SqlTypeName;
            case TypeCode.Boolean:
                return Boolean.SqlTypeName;
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
                return Integer.SqlTypeName;

            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return Decimal.SqlTypeName;
            case TypeCode.String:
                return VarChar.SqlTypeName;
        }

        throw new PostgresOrmException("Unknown SQL Type");
    }
}