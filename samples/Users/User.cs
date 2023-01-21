namespace UsersSampleCode;

public class User
{
    [SQLColumn("PRIMARY KEY")] public BigSerial Id { get; set; }

    [SQLColumn("NOT NULL"), ColumnLength(32)]
    public Varchar Name { get; set; }

    public bool isTeacher { get; set; }
}