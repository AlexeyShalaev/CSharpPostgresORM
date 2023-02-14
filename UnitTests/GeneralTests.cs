using PostgresORM;

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
        Assert.Equals(filter1.ToString(), "((Name = 'Chase' AND Gender != 'female') AND (Age < '40' OR Age > '80'))");


        // testing in select query
        var user1 = new User { Id = 1, Name = "Alex", isTeacher = false };
        var user2 = new User { Id = 2, Name = "Otter18", isTeacher = true };
        await users.Insert(user1);
        await users.Insert(user2);

        {
            var selectQuery1 = await users.Select(users["Name"] == "Alex" | users["isTeacher"] == true);
            Assert.Equals(selectQuery1.Count(), 2);
            var user1_sq1 = selectQuery1.First();
            var user2_sq1 = selectQuery1.Last();
            Assert.Equals(user1_sq1, user1);
            Assert.Equals(user2_sq1, user2);
        }

        {
            var selectQuery2 = await users.Select(users["Name"].Contains("18") & users["isTeacher"] == true);
            Assert.Equals(selectQuery2.Count(), 1);
            var user_sq2 = selectQuery2.First();
            Assert.Equals(user_sq2, user2);
        }

        {
            var selectQuery3 = await users.Select(users["Name"].FinishesWith("18") & users["isTeacher"] != false);
            Assert.Equals(selectQuery3.Count(), 1);
            var user_sq3 = selectQuery3.First();
            Assert.Equals(user_sq3, user2);
        }

        {
            var selectQuery4 = await users.Select(users["Name"].StartsWith("Otter"));
            Assert.Equals(selectQuery4.Count(), 1);
            var user_sq4 = selectQuery4.First();
            Assert.Equals(user_sq4, user2);
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
            Assert.Equals(selectAll.Count(), 2);
            var user1_sa = selectAll.First();
            var user2_sa = selectAll.Last();
            Assert.Equals(user1_sa, user1);
            Assert.Equals(user2_sa, user2);
        }

        // Definite object

        {
            var selectUser1 = await users.Select(user1);
            Assert.Equals(selectUser1.Count(), 1);
            var user_su1 = selectUser1.First();
            Assert.Equals(user_su1, user1);
        }

        // Empty response
        {
            var selectEmpty = await users.Select(new User { Name = "Chase", isTeacher = false });
            CollectionAssert.IsEmpty(selectEmpty);
        }

        // Query

        {
            var selectQuery = await users.Select("Name = 'Otter18'");
            Assert.Equals(selectQuery.Count(), 1);
            var user_sq = selectQuery.First();
            Assert.Equals(user_sq, user2);
        }

        // ----------- update -----------
        await users.Update(user1, ("name", "Aboba"), ("isteacher", "true"));
        var queryResult = await users.Select(users["Id"] == 1);
        var updatedUser1 = queryResult.First();
        Assert.Equals(updatedUser1.Name, "Aboba");
        Assert.Equals(updatedUser1.isTeacher, true);

        // ----------- delete -----------
        await users.Delete();
    }
}