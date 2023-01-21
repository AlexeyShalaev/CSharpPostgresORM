using System.Reflection;
using Dapper;
using Npgsql;
using CSharpPostgresORM.Utils;
namespace CSharpPostgresORM;

public class DataBaseModel<TModel>
{
    private readonly string _connectionString;
    
    public string TableName { get; set; }
    
    public string SchemaName { get; set; }
    
    public NpgsqlConnection Connection { get; set; }

    public DataBaseModel(string connectionString, string tableName, string schemaName = "public")
    {
        TableName = tableName;
        SchemaName = schemaName;
        Connection = new NpgsqlConnection(connectionString);

        _connectionString = connectionString;
        
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            if (SqlType.ToSqlType(propertyInfo.PropertyType) == "" && !SqlType.IsISqlType(propertyInfo))
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
            var flags = Utils.Attributes.GetSqlColumnFlags(propertyInfo);
            if (SqlType.IsISqlType(propertyInfo))
            {
                var columnType = propertyInfo.PropertyType
                    .GetField("SqlTypeName")!.GetValue(propertyInfo.PropertyType);
                if (columnType.ToString().StartsWith("VARCHAR"))
                {
                    columnType += $"({Utils.Attributes.GetColumnLengthAttribute(propertyInfo)})";
                }

                columns.Add(flags is "" ? 
                    $"{propertyInfo.Name} {columnType}" : 
                    $"{propertyInfo.Name} {columnType} {flags}");
            }
            else
            {
                columns.Add(flags is "" ? 
                    $"{propertyInfo.Name} {SqlType.ToSqlType(propertyInfo.PropertyType)}" : 
                    $"{propertyInfo.Name} {SqlType.ToSqlType(propertyInfo.PropertyType)} {flags}");
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
            if (SqlType.IsISqlType(propertyInfo))
            {
                values.Add($"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'");
            }
            else if (SqlType.ToSqlType(propertyInfo.PropertyType) != "")
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

    private string CreateFilter(TModel obj)
    {
        var conditions = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            var condition = $"{propertyInfo.Name} = ";
            if (SqlType.IsISqlType(propertyInfo))
            {
                condition += $"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'";
            }
            else if (SqlType.ToSqlType(propertyInfo.PropertyType) != "")
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
    
    private async Task<int> ExecuteAsync(string query) 
        => await ExecuteAsync(query, null);

    private async Task<int> ExecuteAsync(string query, object? param) 
        => await Connection.ExecuteAsync(query, param);

    private async Task<IEnumerable<dynamic>> QueryAsync(string query)
        => await QueryAsync(query, null);

    private async Task<IEnumerable<dynamic>> QueryAsync(string query, object? param) 
        => await Connection.QueryAsync(query, param);
}