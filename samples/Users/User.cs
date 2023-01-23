using CSharpPostgresORM.Attributes;
using CSharpPostgresORM.SqlTypes;
using Boolean = CSharpPostgresORM.SqlTypes.Boolean;

namespace UsersClassSample;

public class User
{
    [SqlColumn("PRIMARY KEY")] public BigSerial Id { get; set; }

    [SqlColumn("NOT NULL"), ColumnLength(32)]
    public VarChar Name { get; set; }

    public Boolean IsTeacher { get; set; }
}