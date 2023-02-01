using Boolean = PostgresORM.SqlTypes.Binary.Boolean;

namespace UnitTests;

public class SqlTypesTests
{
    [Test]
    public void Boolean()
    {
        Boolean a = true;
        Boolean b = true;

        Assert.That((bool)(a ^ b), Is.False);
    }
}