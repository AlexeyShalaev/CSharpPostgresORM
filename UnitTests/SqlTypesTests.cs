using PostgresORM.SqlTypes.Binary;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;

namespace UnitTests;

public class SqlTypesTests
{
    #region Binary

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
        
        Assert.That((bool)(a ^ true), Is.False);
        Assert.That((bool)(a | false), Is.True);
        Assert.That((bool)(a & false), Is.False);
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
        
        Assert.That((bool)(a ^ true), Is.False);
        Assert.That((bool)(a | false), Is.True);
        Assert.That((bool)(a & false), Is.False);
    }

    [Test]
    public void Binary()
    {
        Binary a = new byte[] { 0, 255, 128, 1 };
        Binary b = new byte[] { 0b1111, 0b1010 };
        Binary c = new byte[] { 0b0101, 0b1111 };

        Assert.That((a ^ a) == new byte[] { 0, 0, 0, 0 }, Is.True);
        Assert.That((b & c) == new byte[] { 0b0101, 0b1010 }, Is.True);
        Assert.That((b | c) == new byte[] { 0b1111, 0b1111 }, Is.True);
        
        Assert.That((a ^ new byte[] { 0, 255, 128, 1 }) == new byte[] { 0, 0, 0, 0 }, Is.True);
        Assert.That((b & new byte[] { 0b0101, 0b1111 }) == new byte[] { 0b0101, 0b1010 }, Is.True);
        Assert.That((b | new byte[] { 0b0101, 0b1111 }) == new byte[] { 0b1111, 0b1111 }, Is.True);
    }

    #endregion

    #region Numeric

    [Test]
    public void BigInteger()
    {
    }

    public void BigSerial()
    {
    }

    public void Decimal()
    {
    }

    public void Integer()
    {
    }

    public void Numeric()
    {
    }

    public void Real()
    {
    }

    public void Serial()
    {
    }

    public void SmallInteger()
    {
    }

    #endregion

    #region String

    [Test]
    public void Character()
    {
    }

    [Test]
    public void Json()
    {
    }

    [Test]
    public void Text()
    {
    }

    [Test]
    public void VarChar()
    {
    }

    #endregion
}