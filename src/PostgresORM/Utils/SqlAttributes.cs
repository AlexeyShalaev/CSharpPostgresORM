﻿using System.Reflection;
using PostgresORM.Attributes;

namespace PostgresORM.Utils;

internal static class SqlAttributes
{
    private const int DefaultVarcharLength = 255;

    /// <summary>
    /// Getting SQL Column Flags [SQLColumn("PRIMARY KEY")]
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    internal static string GetSqlColumnFlags(PropertyInfo propertyInfo)
    {
        var flags = "";
        foreach (var attr in propertyInfo.GetCustomAttributes(true))
        {
            if (attr is SqlColumn sqlAttr)
            {
                flags += sqlAttr.Flag;
            }
        }

        return flags;
    }

    /// <summary>
    /// Getting Sql Column Length [ColumnLength(32)]
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    internal static int GetColumnLengthAttribute(PropertyInfo propertyInfo)
    {
        foreach (var attr in propertyInfo.GetCustomAttributes(true))
        {
            if (attr is ColumnLength sqlAttr)
            {
                return sqlAttr.Length;
            }
        }

        return DefaultVarcharLength;
    }
}