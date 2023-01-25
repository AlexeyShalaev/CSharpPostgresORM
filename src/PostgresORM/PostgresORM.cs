using System.Text.Json;
using Dapper;
using Npgsql;
using PostgresORM.SqlTypes.String;
using PostgresORM.Utils;
using PostgresORM.Utils.SqlFilters;

namespace PostgresORM;

public class DataBaseModel<TModel>
{
    public string TableName { get; set; }

    public string SchemaName { get; set; }

    public NpgsqlConnection Connection { get; }

    public static async Task<DataBaseModel<TModel>> CreateAsync(string server, string username, string password,
        string database, string tableName, string schemaName = "public")
    {
        var connectionString = $"Server={server};Username={username};Password={password};Database={database};";
        var connection = new NpgsqlConnection(connectionString);
        var dataBaseModel = new DataBaseModel<TModel>(connection, tableName, schemaName);
        await dataBaseModel.InitializeAsync();
        return dataBaseModel;
    }

    public static async Task<DataBaseModel<TModel>> CreateAsync(string connectionString, string tableName,
        string schemaName = "public")
    {
        var connection = new NpgsqlConnection(connectionString);
        var dataBaseModel = new DataBaseModel<TModel>(connection, tableName, schemaName);
        await dataBaseModel.InitializeAsync();
        return dataBaseModel;
    }

    public static async Task<DataBaseModel<TModel>> CreateAsync(NpgsqlConnection connection, string tableName,
        string schemaName = "public")
    {
        var dataBaseModel = new DataBaseModel<TModel>(connection, tableName, schemaName);
        await dataBaseModel.InitializeAsync();
        return dataBaseModel;
    }

    private async Task InitializeAsync()
    {
        await CreateSchema();
        await CreateTable();
    }

    private DataBaseModel(NpgsqlConnection connection, string tableName, string schemaName)
    {
        TableName = tableName;
        SchemaName = schemaName;
        Connection = connection;

        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            if (!SqlType.IsSqlType(propertyInfo))
            {
                throw new PostgresOrmException(
                    $"{typeof(TModel)}'s property {propertyInfo} is not inherited from the ISqlType interface or is not a convertible to ISqlType.");
            }
        }
    }

    /*
     * QUERIES BLOCK
     */

    #region CreatingDB

    private async Task<int> CreateSchema()
    {
        var query = $"CREATE SCHEMA IF NOT EXISTS {SchemaName}";
        return await ExecuteAsync(query);
    }

    private async Task<int> CreateTable()
    {
        var columns = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            var flags = SqlAttributes.GetSqlColumnFlags(propertyInfo);
            if (SqlType.IsISqlType(propertyInfo))
            {
                var columnType = propertyInfo.PropertyType
                    .GetField("SqlTypeName")!.GetValue(propertyInfo.PropertyType);
                if (columnType.ToString().StartsWith("VARCHAR"))
                {
                    columnType += $"({SqlAttributes.GetColumnLengthAttribute(propertyInfo)})";
                }

                columns.Add(flags is ""
                    ? $"{propertyInfo.Name} {columnType}"
                    : $"{propertyInfo.Name} {columnType} {flags}");
            }
            else if (SqlType.IsConvertableSqlType(propertyInfo))
            {
                columns.Add(flags is ""
                    ? $"{propertyInfo.Name} {SqlType.GetSqlTypeName(propertyInfo.PropertyType)}"
                    : $"{propertyInfo.Name} {SqlType.GetSqlTypeName(propertyInfo.PropertyType)} {flags}");
            }
        }

        var query = $"CREATE TABLE IF NOT EXISTS {SchemaName}.{TableName} ({string.Join(", ", columns)});";
        Console.WriteLine(query);
        return await ExecuteAsync(query);
    }

    #endregion

    #region Inserting

    public async Task<int> Insert(TModel obj)
    {
        var values = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            if (SqlType.IsISqlType(propertyInfo))
            {
                var columnType = propertyInfo.PropertyType.GetField("SqlTypeName")!.GetValue(propertyInfo.PropertyType);
                if (columnType is "JSON")
                {
                    if (propertyInfo.GetValue(obj) is null)
                    {
                        values.Add($"DEFAULT");
                    }
                    else
                    {
                        var value = propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj));
                        values.Add($"'{JsonSerializer.Serialize(value)}'");
                    }
                }
                else
                {
                    if (propertyInfo.GetValue(obj) is null)
                    {
                        values.Add($"DEFAULT");
                    }
                    else
                    {
                        values.Add(
                            $"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'");
                    }
                }
            }
            else if (SqlType.IsConvertableSqlType(propertyInfo))
            {
                if (propertyInfo.GetValue(obj) is null)
                {
                    values.Add($"DEFAULT");
                }
                else
                {
                    values.Add($"'{propertyInfo.GetValue(obj)}'");
                }
            }
            else
            {
                throw new PostgresOrmException($"Can't get value from property {propertyInfo}");
            }
        }

        var query =
            $"INSERT INTO {SchemaName}.{TableName}({GetColumns()}) VALUES({string.Join(", ", values)});";
        Console.WriteLine(query);
        return await ExecuteAsync(query);
    }

    #endregion

    #region Selecting

    public SqlColumnFilter this[string columnName] => new SqlColumnFilter(columnName);

    public async Task<IEnumerable<TModel>> Select(SqlFilter filter)
    {
        var query = CreateQuery("SELECT *", filter.ToString());
        Console.WriteLine(query);
        return await QueryAsync(query);
    }


    public async Task<IEnumerable<TModel>> Select(TModel obj)
    {
        var query = CreateQuery("SELECT *", CreateFilter(obj));
        Console.WriteLine(query);
        return await QueryAsync(query);
    }

    public async Task<IEnumerable<TModel>> Select(string queryCondition = "")
    {
        var query = CreateQuery("SELECT *", queryCondition);
        Console.WriteLine(query);
        return await QueryAsync(query);
    }

    #endregion

    #region Deleting

    public async Task<int> Delete(SqlFilter filter)
    {
        var query = CreateQuery("DELETE", filter.ToString());
        return await ExecuteAsync(query);
    }

    public async Task<int> Delete(TModel obj)
    {
        var query = CreateQuery("DELETE", CreateFilter(obj));
        return await ExecuteAsync(query);
    }

    public async Task<int> Delete(string queryCondition = "")
    {
        var query = CreateQuery("DELETE", queryCondition);
        return await ExecuteAsync(query);
    }

    #endregion

    /*
     * UTILS
     */

    #region OrmUtils

    private string CreateFilter(TModel obj)
    {
        var conditions = new List<string>();
        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            var condition = $"{propertyInfo.Name} = ";
            if (SqlType.IsISqlType(propertyInfo))
            {
                if (propertyInfo.GetValue(obj) is not null)
                {
                    condition +=
                        $"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'";
                }
                else
                {
                    continue;
                }
            }
            else if (SqlType.IsConvertableSqlType(propertyInfo))
            {
                var value = propertyInfo.GetValue(obj);
                if (value is null) continue;
                condition += $"'{value}'";
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
            return $"{command} FROM {SchemaName}.{TableName};";
        }

        return $"{command} FROM {SchemaName}.{TableName} WHERE {filter};";
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

    #endregion

    /*
     * POSTGRES
     */

    #region PostGresExecutors

    private async Task<int> ExecuteAsync(string query)
        => await ExecuteAsync(query, null);

    private async Task<int> ExecuteAsync(string query, object? param)
        => await Connection.ExecuteAsync(query, param);

    private async Task<IEnumerable<TModel>> QueryAsync(string query)
        => await QueryAsync(query, null);

    private async Task<IEnumerable<TModel>> QueryAsync(string query, object? param)
        => await Connection.QueryAsync<TModel>(query, param);

    #endregion
}