using PostgresORM;
using UsersClassSample;


const string connectionString = "Server = localhost;" +
                                "Username=test;" +
                                "Password=test;" +
                                "Database=test;";

var users = await DataBaseModel<User>.CreateAsync(connectionString, "test", "test");

var user1 = new User { Id = 1, Name = "Alex", IsTeacher = false };
var user2 = new User { Id = 2, Name = "Otter18", IsTeacher = true };

//----------- insert -----------
await users.Insert(user1);
await users.Insert(user2);

// ----------- select -----------

// All
var selectAll = await users.Select();

// Definite
var selectUser1 = await users.Select(user1);

// Query
var selectQuery = await users.Select("Name = 'Otter18'");
var selectQuery1 = await users.Select(users["Name"] == "Alex" | users["isTeacher"] == true);
var selectQuery2 = await users.Select(users["Name"].Contains("18") & users["isTeacher"] == true);
var selectQuery3 = await users.Select(users["Name"].FinishesWith("18") & users["isTeacher"] != false);
var selectQuery4 = await users.Select(users["Name"].StartsWith("Otter"));

// ----------- update -----------
await users.Update(users["Name"] == "Alex", ("name", "Glinomes"), ("isteacher", "true"));
await users.Update(user1, ("name", "Stepashka"), ("isteacher", "true"));
await users.Update(user2, "name", "Portyanka");

// ----------- delete -----------
await users.Delete(users["Name"] == "Alex");
await users.Delete(users["Name"] == "Otter18");

await users.Delete();