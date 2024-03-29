﻿using PostgresORM.Attributes;
using PostgresORM.SqlTypes.Numeric;
using PostgresORM.SqlTypes.String;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;

namespace UsersClassSample;

public class User
{
    [SqlColumn("PRIMARY KEY")] public BigSerial Id { get; set; }

    [SqlColumn("NOT NULL"), ColumnLength(32)]
    public VarChar Name { get; set; }

    public Boolean IsTeacher { get; set; }
}