﻿namespace CSharpPostgresORM;
using System.Reflection;

public class DataBaseModel<TModel>
{
    public string TableName { get; set; }
    
    public string SchemaName { get; set; }

    public DataBaseModel(string tableName, string schemaName = "public")
    {
        TableName = tableName;
        SchemaName = schemaName;

        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            if (ToSqlType(propertyInfo.PropertyType) == "" && !IsISqlType(propertyInfo))
            {
                throw new Exception($"{typeof(TModel)}'s property {propertyInfo} is not inherited from the interface.");
            }
        }

        //if (typeof(TModel).GetProperties().All(IsISqlType) is false)
        //   throw new Exception($"{typeof(TModel)}'s property is not inherited from the interface.");
    }

    /*
     * QUERIES BLOCK
     */

    public void CreateSchema()
    {
        var query = $"CREATE SCHEMA IF NOT EXISTS {SchemaName}";
        Console.WriteLine(query);
    }
    
    public void CreateTable()
    {
        var columns = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            var flags = GetFlags(propertyInfo);
            if (IsISqlType(propertyInfo))
            {
                var columnType = propertyInfo.PropertyType.GetField("SqlTypeName").GetValue(propertyInfo.PropertyType);
                if (columnType.ToString().StartsWith("VARCHAR"))
                {
                    columnType += $"({GetLengthAttribute(propertyInfo)})";
                }

                columns.Add(flags is "" ? 
                    $"{propertyInfo.Name} {columnType}" : 
                    $"{propertyInfo.Name} {columnType} {flags}");
            }
            else
            {
                columns.Add(flags is "" ? 
                    $"{propertyInfo.Name} {ToSqlType(propertyInfo.PropertyType)}" : 
                    $"{propertyInfo.Name} {ToSqlType(propertyInfo.PropertyType)} {flags}");
            }
        }

        var query = $"CREATE TABLE IF NOT EXISTS {SchemaName}.{TableName} ({string.Join(", ", columns)});";
        Console.WriteLine(query);
    }

    public void Insert(TModel obj)
    {
        var values = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            if (IsISqlType(propertyInfo))
            {
                values.Add($"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'");
            }
            else if (ToSqlType(propertyInfo.PropertyType) != "")
            {
                values.Add($"'{propertyInfo.GetValue(obj)}'");
            }
            else
            {
                // TODO: check if attributes contains not null
                values.Add($"'NULL'");
            }
        }

        var query =
            $"INSERT INTO {SchemaName}.{TableName}({GetColumns()}) VALUES({string.Join(", ", values)});";
        Console.WriteLine(query);
    }

    public void Select(TModel obj)
    {
        var query = CreateQuery("SELECT *", CreateFilter(obj));
        Console.WriteLine(query);
    }

    public void Select(string queryCondition = "")
    {
        var query = CreateQuery("SELECT *", queryCondition);
        Console.WriteLine(query);
    }

    public void Delete(TModel obj)
    {
        var query = CreateQuery("DELETE", CreateFilter(obj));
        Console.WriteLine(query);
    }

    public void Delete(string queryCondition = "")
    {
        var query = CreateQuery("DELETE", queryCondition);
        Console.WriteLine(query);
    }


    /*
     * UTILS BLOCK
     */

    private string CreateFilter(TModel obj)
    {
        var conditions = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            var condition = $"{propertyInfo.Name} = ";
            if (IsISqlType(propertyInfo))
            {
                condition += $"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'";
            }
            else if (ToSqlType(propertyInfo.PropertyType) != "")
            {
                condition += $"'{propertyInfo.GetValue(obj)}'";
            }
            else
            {
                continue;
            }

            conditions.Add(condition);
        }

        return string.Join(" AND ", conditions);
    }

    private string CreateQuery(string command, string filter = "")
    {
        if (filter == "")
        {
            return $"{command} FROM public.{TableName};";
        }

        return $"{command} FROM public.{TableName} WHERE {filter};";
    }

    private string GetColumns()
    {
        var columns = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            columns.Add($"{propertyInfo.Name}");
        }

        return string.Join(", ", columns);
    }

    private static string GetFlags(PropertyInfo propertyInfo)
    {
        var flags = "";
        foreach (var attr in propertyInfo.GetCustomAttributes(true))
        {
            var sqlAttr = attr as SQLColumn;
            if (sqlAttr != null)
            {
                flags += sqlAttr.Flag;
            }
        }

        return flags;
    }

    private static int GetLengthAttribute(PropertyInfo propertyInfo)
    {
        foreach (var attr in propertyInfo.GetCustomAttributes(true))
        {
            var sqlAttr = attr as ColumnLength;
            if (sqlAttr != null)
            {
                return sqlAttr.Length;
            }
        }

        return 255;
    }

    private bool IsISqlType(PropertyInfo propertyInfo)
    {
        //return propertyInfo.PropertyType.GetInterfaces().Contains(typeof(ISqlType));
        return typeof(ISqlType).IsAssignableFrom(propertyInfo.PropertyType);
    }

    private static string ToSqlType(Type type)
    {
        var typeCode = Type.GetTypeCode(type);
        var name = "";

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
                name = "INTEGER";
                break;
            case TypeCode.Boolean:
                name = "BOOLEAN";
                break;
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                name = "DECIMAL";
                break;
            case TypeCode.String:
                name = "TEXT";
                break;
        }

        return name;
    }
}