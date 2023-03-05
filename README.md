# CSharpPostgresORM

Your friendly neighborhood CSharp Postgres ORM

[![Build status](https://github.com/App-vNext/Polly/workflows/build/badge.svg?branch=main&event=push)](https://github.com/App-vNext/Polly/actions?query=workflow%3Abuild+branch%3Amain+event%3Apush)

## Install

To install CSPORM, use the following command in the Package Manager Console

    PM> Install-Package csporm

# Sample

## class `User`

```csharp
public class User
{
    [SqlColumn("PRIMARY KEY")] 
    public BigSerial Id { get; set; }

    [SqlColumn("NOT NULL"), ColumnLength(32)]
    public VarChar Name { get; set; }

    public Boolean IsTeacher { get; set; }
}
```

## Init DataModel class

```csharp
const string connectionString = "Server = localhost;" +
                                "Username=test;" +
                                "Password=test;" +
                                "Database=test;";

var users = await DataBaseModel<User>.CreateAsync(connectionString, "test", "test");
```

## Create & insert users

```csharp
var user1 = new User { Id = 1, Name = "Alex", IsTeacher = false };
var user2 = new User { Id = 2, Name = "Otter18", IsTeacher = true };

//----------- insert -----------
await users.Insert(user1);
await users.Insert(user2);
```

## Select queries

### Select all

```csharp
var selectAll = await users.Select();
```

### Select by user object

```csharp
var selectUser1 = await users.Select(user1);
```

### Select by string filter or SqlFilter

```csharp
var selectQuery = await users.Select("Name = 'Otter18'");
var selectQuery1 = await users.Select(users["Name"] == "Alex" | users["isTeacher"] == true);
var selectQuery2 = await users.Select(users["Name"].Contains("18") & users["isTeacher"] == true);
var selectQuery3 = await users.Select(users["Name"].FinishesWith("18") & users["isTeacher"] != false);
var selectQuery4 = await users.Select(users["Name"].StartsWith("Otter"));
```

## Updating

### Based on filter

```csharp
await users.Update(users["Name"] == "Alex", ("name", "Glinomes"), ("isteacher", "true"));
```

### Update by user object

```csharp
await users.Update(user1, ("name", "Stepashka"), ("isteacher", "true"));
await users.Update(user2, "name", "Portyanka");
```

## Delete

### Based on filter

```csharp
await users.Delete(users["Name"] == "Alex");
await users.Delete(users["Name"] == "Otter18");
```

### Empty filter = all  rows

```csharp
await users.Delete();
```

# Documentation

## Connection to Data Base

```csharp
public static async Task<DataBaseModel<TModel>> CreateAsync(string server, string username, string password, string database, string tableName, string schemaName = "public");
public static async Task<DataBaseModel<TModel>> CreateAsync(string connectionString, string tableName, string schemaName = "public");
public static async Task<DataBaseModel<TModel>> CreateAsync(NpgsqlConnection connection, string tableName, string schemaName = "public");
```

## Properties

```csharp
public string TableName { get; set; }

public string SchemaName { get; set; }

public NpgsqlConnection Connection { get; }
```

## Attributes

```csharp
[SqlColumn("PRIMARY KEY")] // postgres limitations, for example: NOT NULL
[ColumnLength(32)] // used for VarChar
```

## Models

!!!WARNING!!!<br />
If you do not specify a value for the CSharp type insrting the model to the table, then it will be filled with default.<br />
Boolean field // OK<br />
bool field => false // Only if it doesn't violate your logic<br />
bool? field => null // OK<br />

```csharp
public class Example
{
    public Integer Int_1 { get; set; } // SqlType

    public int Int_2 { get; set; }      // CSharp Type 
    
    public int? Int_3 { get; set; }
}
```

## Queries

### Inserting

```csharp
public async Task<int> Insert(TModel obj);
```

### Selecting

```csharp
public async Task<IEnumerable<TModel>> Select(SqlFilter filter);
public async Task<IEnumerable<TModel>> Select(TModel obj);
public async Task<IEnumerable<TModel>> Select(string queryCondition = "");
```

### Updating

```csharp
public async Task<int> Update(SqlFilter filter, string key, string value);
public async Task<int> Update(SqlFilter filter, params ValueTuple<string, string>[] data);
public async Task<int> Update(TModel obj, string key, string value);
public async Task<int> Update(TModel obj, params ValueTuple<string, string>[] data);
public async Task<int> Update(string setCondition, string whereCondition);
```
### Deleting

```csharp
public async Task<int> Delete(SqlFilter filter);
public async Task<int> Delete(TModel obj);
public async Task<int> Delete(string queryCondition = "");
```

## Filters
TODO: VOVA

## SqlTypes

You can create your own data type inherited from the ISqlType interface that supports postgres.

```csharp
public class MyType : ISqlType<object>
```

### ISqlType

```csharp
public interface ISqlType<T> : ISqlType
{
    public T Value { get; set; }
}
```

### Numeric
| ORM Type | CSharp Type |
| --- | --- |
| BigInteger | long |
| BigSerial | long |
| Real | float |
| Decimal | decimal |
| Numeric | decimal |
| Integer | int |
| Serial | uint |
| SmallInteger | short |

### Binary
| ORM Type | CSharp Type |
| --- | --- |
| Binary | byte[] |
| Bit | bool |
| Boolean | bool |

### String
| ORM Type | CSharp Type |
| --- | --- |
| Character | char |
| Text | string |
| VarChar | string |
| Json<T> | T |
