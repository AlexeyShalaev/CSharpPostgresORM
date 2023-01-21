using CSharpPostgresORM.Utils;
using Dapper;
using Npgsql;

namespace CSharpPostgresORM;

public class DataBaseModel<TModel>
{
    private readonly string _connectionString;

    public string TableName { get; set; }

    public string SchemaName { get; set; }

    public NpgsqlConnection Connection { get; set; }

    public static async Task<DataBaseModel<TModel>> CreateAsync(string connectionString, string tableName,
        string schemaName = "public")
    {
        var dataBaseModel = new DataBaseModel<TModel>(connectionString, tableName, schemaName);
        await dataBaseModel.InitializeAsync();
        return dataBaseModel;
    }

    private async Task InitializeAsync()
    {
        await CreateSchema();
        await CreateTable();
    }

    private DataBaseModel(string connectionString, string tableName, string schemaName)
    {
        TableName = tableName;
        SchemaName = schemaName;
        Connection = new NpgsqlConnection(connectionString);

        _connectionString = connectionString;

        foreach (var propertyInfo in typeof(TModel).GetProperties())
        {
            if (!SqlType.IsSqlType(propertyInfo))
            {
                throw new PostgresOrmException(
                    $"{typeof(TModel)}'s property {propertyInfo} is not inherited from the interface.");
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
                values.Add($"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'");
            }
            else if (SqlType.IsConvertableSqlType(propertyInfo))
            {
                values.Add($"'{propertyInfo.GetValue(obj)}'");
            }

            throw new PostgresOrmException($"Can't get value from property {propertyInfo}");
        }

        var query =
            $"INSERT INTO {SchemaName}.{TableName}({GetColumns()}) VALUES({string.Join(", ", values)});";
        return await ExecuteAsync(query);
    }

    #endregion

    #region Selecting

    public async Task<IEnumerable<dynamic>> Select(TModel obj)
    {
        var query = CreateQuery("SELECT *", CreateFilter(obj));
        return await QueryAsync(query);
    }

    public async Task<IEnumerable<dynamic>> Select(string queryCondition = "")
    {
        var query = CreateQuery("SELECT *", queryCondition);
        return await QueryAsync(query);
    }

    #endregion

    #region Deleting

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
                condition += $"'{propertyInfo.PropertyType.GetProperty("Value").GetValue(propertyInfo.GetValue(obj))}'";
            }
            else if (SqlType.IsConvertableSqlType(propertyInfo))
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

    #endregion

    /*
     * POSTGRES
     */

    #region PostGresExecutors

    private async Task<int> ExecuteAsync(string query)
        => await ExecuteAsync(query, null);

    private async Task<int> ExecuteAsync(string query, object? param)
        => await Connection.ExecuteAsync(query, param);

    private async Task<IEnumerable<dynamic>> QueryAsync(string query)
        => await QueryAsync(query, null);

    private async Task<IEnumerable<dynamic>> QueryAsync(string query, object? param)
        => await Connection.QueryAsync(query, param);

    #endregion
}