using CSharpPostgresORM;

namespace UnitTests;

public class Tests
{
    private string _connectionString = "Server = 192.168.1.57;" +
                                       "Username=test;" +
                                       "Password=test;" +
                                       "Database=test;";


    [SetUp]
    public async Task Setup()
    {
        var users = await DataBaseModel<User>.CreateAsync(_connectionString, "test", "test");
        await users.Delete();
    }

    [Test]
    public async Task SqlFilterTest()
    {
        var users = await DataBaseModel<User>.CreateAsync(_connectionString, "test", "test");

        // testing filters combinations in general
        var filter1 = users["Name"] == "Chase" &
                      users["Gender"] != "female" &
                      (users["Age"] < 40 | users["Age"] > 80);
        Console.WriteLine(filter1);
        Assert.That(filter1.ToString(),
            Is.EqualTo("((Name = 'Chase' AND Gender != 'female') AND (Age < '40' OR Age > '80'))"));


        // testing in select query
        var user1 = new User { Id = 1, Name = "Alex", isTeacher = false };
        var user2 = new User { Id = 2, Name = "Otter18", isTeacher = true };
        await users.Insert(user1);
        await users.Insert(user2);

        var selectQuery1 = await users.Select(users["Name"] == "Alex" | users["isTeacher"] == true);
        Assert.That(selectQuery1.Count(), Is.EqualTo(2)); // TODO compare actual data

        var selectQuery2 = await users.Select(users["Name"].Contains("18") & users["isTeacher"] == true);
        Assert.That(selectQuery2.Count(), Is.EqualTo(1)); // TODO compare actual data

        var selectQuery3 = await users.Select(users["Name"].FinishesWith("18") & users["isTeacher"] != false);
        Assert.That(selectQuery3.Count(), Is.EqualTo(1)); // TODO compare actual data

        var selectQuery4 = await users.Select(users["Name"].StartsWith("Otter"));
        Assert.That(selectQuery4.Count(), Is.EqualTo(1)); // TODO compare actual data

        // testing in delete query
        await users.Delete(users["Name"] == "Alex");
        await users.Delete(users["Name"] == "Otter18");

        CollectionAssert.IsEmpty(await users.Select());
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

        // Select all
        var selectAll = await users.Select();
        Assert.That(selectAll.Count(), Is.EqualTo(2)); // TODO compare actual data 

        // Definite object
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