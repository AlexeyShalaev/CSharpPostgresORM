using CSharpPostgresORM;

namespace UnitTests;

public class Tests
{
    private string _connectionString = "";

    [SetUp]
    public void Setup()
    {
        _connectionString =
            "Server=localhost;" +
            "Username=test;" +
            "Password=test;" +
            "Database=test;";
    }

    [Test]
    public async Task Test1()
    {
        var users = await DataBaseModel<User>.CreateAsync(_connectionString, "test", "test");

        var user1 = new User { Id = 1, Name = "Alex", isTeacher = false };
        var user2 = new User { Id = 2, Name = "Otter18", isTeacher = true };

        //----------- insert -----------
        await users.Insert(user1);
        await users.Insert(user2);

        // ----------- select -----------

        // All
        var selectAll = await users.Select();
        Assert.That(selectAll.Count(), Is.EqualTo(2)); // TODO compare actual data 

        // Definite
        var selectUser1 = await users.Select(user1);
        Assert.That(selectUser1.Count(), Is.EqualTo(1)); // TODO compare actual data 

        // Empty response
        var selectEmpty = await users.Select(new User { Name = "Chase", isTeacher = false });
        CollectionAssert.IsEmpty(selectEmpty);

        // Query
        var selectQuery = await users.Select("Name = 'Otter18'");
        Assert.That(selectQuery.Count(), Is.EqualTo(1)); // TODO compare actual data 

        // ----------- delete -----------
        await users.Delete();
    }
}