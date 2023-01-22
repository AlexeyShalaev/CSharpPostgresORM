namespace UnitTests;

public class User
{
    [SqlColumn("PRIMARY KEY")] public BigSerial Id { get; set; }

    [SqlColumn("NOT NULL"), ColumnLength(32)]
    public VarChar Name { get; set; }

    public bool isTeacher { get; set; }
}