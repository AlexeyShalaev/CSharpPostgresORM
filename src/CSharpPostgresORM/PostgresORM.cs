namespace CSharpPostgresORM;

class DataBaseModel<TModel>
{
    public string connectionString { get; set; }
    public string TableName { get; set; }

    public DataBaseModel(string tableName)
    {
        TableName = tableName;

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
                    columnType += $"({Attributes.GetColumnLengthAttribute(propertyInfo)})";
                }

                columns.Add(
                    $"{propertyInfo.Name} {columnType} {flags}");
            }
            else
            {
                columns.Add($"{propertyInfo.Name} {ToSqlType(propertyInfo.PropertyType)} {flags}");
            }
        }

        var query = $"CREATE TABLE IF NOT EXISTS public.{TableName} ({string.Join(", ", columns)});";
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
            $"INSERT INTO public.{TableName}({GetColumns()}) VALUES({string.Join(", ", values)});";
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
}