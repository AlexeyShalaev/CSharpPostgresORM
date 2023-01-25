using System.Reflection;
using System.Text.RegularExpressions;
using PostgresORM.SqlTypes;
using PostgresORM.SqlTypes.Binary;
using PostgresORM.SqlTypes.Numeric;
using PostgresORM.SqlTypes.String;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;
using Decimal = PostgresORM.SqlTypes.Numeric.Decimal;

namespace PostgresORM.Utils;

internal static class SqlType
{
    public static bool IsISqlType(PropertyInfo propertyInfo)
    {
        return typeof(ISqlType).IsAssignableFrom(propertyInfo.PropertyType);
    }


    public static bool IsConvertableSqlType(PropertyInfo propertyInfo)
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

    public static bool IsSqlType(PropertyInfo propertyInfo)
    {
        return IsISqlType(propertyInfo) || IsConvertableSqlType(propertyInfo);
    }


    public static string GetSqlTypeName(Type type)
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

        return Json<TypeCode>.SqlTypeName;
        throw new PostgresOrmException("Unknown SQL Type");
    }

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