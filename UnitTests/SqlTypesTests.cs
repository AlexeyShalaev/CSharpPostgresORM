using PostgresORM.SqlTypes.Binary;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;
using PostgresORM.SqlTypes.Numeric;
using Decimal = PostgresORM.SqlTypes.Numeric.Decimal;

namespace UnitTests;

public class SqlTypesTests
{

    // Binary

    [Test]
    public void Boolean()
    {
        Boolean a = true;
        Boolean b = true;
        Boolean c = false;

        Assert.That((bool)(a ^ b), Is.False);
        Assert.That((bool)(a | c), Is.True);
        Assert.That((bool)(a & c), Is.False);
        Assert.That((bool)~a, Is.False);
        Assert.That((bool)~c, Is.True);
    }

    [Test]
    public void Bit()
    {
        Bit a = true;
        Bit b = true;
        Bit c = false;

        Assert.That((bool)(a ^ b), Is.False);
        Assert.That((bool)(a | c), Is.True);
        Assert.That((bool)(a & c), Is.False);
        Assert.That((bool)~a, Is.False);
        Assert.That((bool)~c, Is.True);
    }

    [Test]
    public void Binary()
    {
        Binary a = new byte[] { 0, 255, 128, 1 };
        Binary b = new byte[] { 0b1111, 0b1010};
        Binary c = new byte[] { 0b0101, 0b1111};

        Assert.That((a ^ a) == new byte[] { 0, 0, 0, 0 }, Is.True);
        Assert.That((b & c) == new byte[] { 0b0101, 0b1010}, Is.True);
        Assert.That((b | c) == new byte[] { 0b1111, 0b1111}, Is.True);
    }

    // Numeric

    [Test]
    public void BigInteger()
    {
        long aValue = 10000000;
        long bValue = 100000000;
        long cValue = 100000000;
        BigInteger a = aValue;
        BigInteger b = bValue;
        BigInteger c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void BigSerial()
    {
        ulong aValue = 10000000;
        ulong bValue = 100000000;
        ulong cValue = 100000000;
        BigSerial a = aValue;
        BigSerial b = bValue;
        BigSerial c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void Decimal()
    {
        decimal aValue = 10000000;
        decimal bValue = 100000000;
        decimal cValue = 100000000;
        Decimal a = aValue;
        Decimal b = bValue;
        Decimal c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void Integer()
    {
        int aValue = 10000000;
        int bValue = 100000000;
        int cValue = 100000000;
        Integer a = aValue;
        Integer b = bValue;
        Integer c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void Numeric()
    {
        decimal aValue = 10000000;
        decimal bValue = 100000000;
        decimal cValue = 100000000;
        Numeric a = aValue;
        Numeric b = bValue;
        Numeric c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void Real()
    {
        float aValue = 10000000;
        float bValue = 100000000;
        float cValue = 100000000;
        Real a = aValue;
        Real b = bValue;
        Real c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void Serial()
    {
        uint aValue = 10000000;
        uint bValue = 100000000;
        uint cValue = 100000000;
        Serial a = aValue;
        Serial b = bValue;
        Serial c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }

    [Test]
    public void SmallInteger()
    {
        short aValue = 1000;
        short bValue = 10000;
        short cValue = 10000;
        SmallInteger a = aValue;
        SmallInteger b = bValue;
        SmallInteger c = cValue;

        Assert.That(b == c, Is.True);
        Assert.That(b > a, Is.True);
        Assert.That(a < b, Is.True);
        Assert.That(a > b, Is.False);
        Assert.That(b - c == 0, Is.True);
        Assert.That(a + b == aValue + bValue, Is.True);
        Assert.That(b - a == bValue - aValue, Is.True);
    }
}