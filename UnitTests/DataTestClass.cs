using PostgresORM.Attributes;
using PostgresORM.SqlTypes.Numeric;
using PostgresORM.SqlTypes.String;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;

namespace UnitTests;

public class User
{
    [SqlColumn("PRIMARY KEY")] public BigSerial Id { get; set; }

    [SqlColumn("NOT NULL"), ColumnLength(32)]
    public VarChar Name { get; set; }

    public Boolean isTeacher { get; set; }

    public bool Equals(User other)
    {
        return Id == other.Id && Name == other.Name && isTeacher == other.isTeacher;
    }
}