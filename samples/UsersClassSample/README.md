# PostgresORM sample

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
