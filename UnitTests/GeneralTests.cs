using PostgresORM;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;
using PostgresORM.SqlTypes.String;


namespace UnitTests;

public class GeneralTests
{
    private string _connectionString = "Server=localhost;" +
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
        Assert.That("((Name = 'Chase' AND Gender != 'female') AND (Age < '40' OR Age > '80'))",
            Is.EqualTo(filter1.ToString()));


        // testing in select query
        var user1 = new User { Id = 1, Name = "Alex", isTeacher = false };
        var user2 = new User { Id = 2, Name = "Otter18", isTeacher = true };
        await users.Insert(user1);
        await users.Insert(user2);

        {
            var selectQuery1 = await users.Select(users["Name"] == "Alex" | users["isTeacher"] == true);
            Assert.That(2, Is.EqualTo(selectQuery1.Count()));
            var user1_sq1 = selectQuery1.First();
            var user2_sq1 = selectQuery1.Last();
            Assert.That(user1, Is.EqualTo(user1_sq1));
            Assert.That(user2, Is.EqualTo(user2_sq1));
        }

        {
            var selectQuery2 = await users.Select(users["Name"].Contains("18") & users["isTeacher"] == true);
            Assert.That(1, Is.EqualTo(selectQuery2.Count()));
            var user_sq2 = selectQuery2.First();
            Assert.That(user2, Is.EqualTo(user_sq2));
        }

        {
            var selectQuery3 = await users.Select(users["Name"].FinishesWith("18") & users["isTeacher"] != false);
            Assert.That(1, Is.EqualTo(selectQuery3.Count()));
            var user_sq3 = selectQuery3.First();
            Assert.That(user2, Is.EqualTo(user_sq3));
        }

        {
            var selectQuery4 = await users.Select(users["Name"].StartsWith("Otter"));
            Assert.That(1, Is.EqualTo(selectQuery4.Count()));
            var user_sq4 = selectQuery4.First();
            Assert.That(user2, Is.EqualTo(user_sq4));
        }

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

        {
            var selectAll = await users.Select();
            Assert.That(2, Is.EqualTo(selectAll.Count()));
            var user1_sa = selectAll.First();
            var user2_sa = selectAll.Last();
            Assert.True(user1.Equals(user1_sa));
            Assert.True(user2.Equals(user2_sa));
        }

        // Definite object

        {
            var selectUser1 = await users.Select(user1);
            Assert.That(1, Is.EqualTo(selectUser1.Count()));
            var user_su1 = selectUser1.First();
            Assert.True(user1.Equals(user_su1));
        }

        // Empty response
        {
            var selectEmpty = await users.Select(new User { Name = "Chase", isTeacher = false });
            CollectionAssert.IsEmpty(selectEmpty);
        }

        // Query

        {
            var selectQuery = await users.Select("Name = 'Otter18'");
            Assert.That(1, Is.EqualTo(selectQuery.Count()));
            var user_sq = selectQuery.First();
            Assert.True(user2.Equals(user_sq));
        }

        // ----------- update -----------
        await users.Update(user1, ("name", "Aboba"), ("isteacher", "true"));
        var queryResult = await users.Select(users["Id"] == 1);
        var updatedUser1 = queryResult.First();
        Assert.That((VarChar)"Aboba", Is.EqualTo(updatedUser1.Name));
        Assert.That((Boolean)true, Is.EqualTo(updatedUser1.isTeacher));

        // ----------- delete -----------
        await users.Delete();
    }
}