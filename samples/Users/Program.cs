using PostgresORM;

namespace UsersSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new DataBaseModel<User>("users_table");
            users.CreateTable();
            users.Insert(new User { Id = 1, Name = "Alex", isTeacher = false });
//select
            users.Select(); //All
            users.Select(new User { Id = 1, Name = "Alex", isTeacher = false }); //Definite
            users.Select("Id = '1' OR Name = 'Alex'"); //Query
//delete
            users.Delete(); //All
            users.Delete(new User { Id = 1, Name = "Alex", isTeacher = false }); //Definite
            users.Delete("Id = '1' OR Name = 'Alex'"); //Query
        }
    }
}

