using Boolean = PostgresORM.SqlTypes.Binary.Boolean;

namespace UnitTests;

public class SqlTypesTests
{
    [Test]
    public void SqlFilterTestBoolean()
    {
        Boolean a = true;
        Boolean b = true;

        Assert.That((bool)(a ^ b), Is.False);
    }
}