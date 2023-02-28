using PostgresORM.SqlTypes.Binary;
using PostgresORM.SqlTypes.Numeric;
using Boolean = PostgresORM.SqlTypes.Binary.Boolean;
using PostgresORM.SqlTypes.Numeric;
using Decimal = PostgresORM.SqlTypes.Numeric.Decimal;

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
        long a = 57;
        long b = 179;

        BigInteger a_ = a;
        BigInteger b_ = b;

        Assert.That((long)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((long)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((long)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((long)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((long)(b_ % a_), Is.EqualTo(b % a));
        
        Assert.That((long)++a_, Is.EqualTo(++a));
        Assert.That((long)-a_, Is.EqualTo(-a));
        Assert.That((long)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void BigSerial()
    {
        long a = 57;
        long b = 179;

        BigSerial a_ = a;
        BigSerial b_ = b;

        Assert.That((long)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((long)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((long)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((long)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((long)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((long)++a_, Is.EqualTo(++a));
        Assert.That((long)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void Decimal()
    {
        decimal a = 57;
        decimal b = 179;

        Decimal a_ = a;
        Decimal b_ = b;

        Assert.That((decimal)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((decimal)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((decimal)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((decimal)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((decimal)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((decimal)++a_, Is.EqualTo(++a));
        Assert.That((decimal)-a_, Is.EqualTo(-a));
        Assert.That((decimal)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void Integer()
    {
        int a = 57;
        int b = 179;

        Integer a_ = a;
        Integer b_ = b;

        Assert.That((int)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((int)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((int)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((int)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((int)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((int)++a_, Is.EqualTo(++a));
        Assert.That((int)-a_, Is.EqualTo(-a));
        Assert.That((int)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void Numeric()
    {
        decimal a = 57;
        decimal b = 179;

        Numeric a_ = a;
        Numeric b_ = b;

        Assert.That((decimal)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((decimal)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((decimal)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((decimal)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((decimal)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((decimal)++a_, Is.EqualTo(++a));
        Assert.That((decimal)-a_, Is.EqualTo(-a));
        Assert.That((decimal)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void Real()
    {
        float a = 57;
        float b = 179;

        Real a_ = a;
        Real b_ = b;

        Assert.That((float)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((float)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((float)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((float)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((float)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((float)++a_, Is.EqualTo(++a));
        Assert.That((float)-a_, Is.EqualTo(-a));
        Assert.That((float)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void Serial()
    {
        uint a = 57;
        uint b = 179;

        Serial a_ = a;
        Serial b_ = b;

        Assert.That((uint)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((uint)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((uint)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((uint)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((uint)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((uint)++a_, Is.EqualTo(++a));
        Assert.That((uint)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    [Test]
    public void SmallInteger()
    {
        short a = 57;
        short b = 179;

        SmallInteger a_ = a;
        SmallInteger b_ = b;

        Assert.That((short)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((short)(a_ - b_), Is.EqualTo(a - b));
        Assert.That((short)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((short)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((short)(b_ % a_), Is.EqualTo(b % a));

        Assert.That((short)++a_, Is.EqualTo(++a));
        Assert.That((short)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ != b_, Is.True);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
    }

    #endregion

    #region String

    [Test]
    public void Character()
    {
        char a = 'a';
        char b = 'z';

        Character a_ = a;
        Character b_ = b;

        Assert.That((char)(a_ + b_), Is.EqualTo(a + b));
        Assert.That((char)(b_ - a_), Is.EqualTo(b - a));
        Assert.That((char)(a_ * b_), Is.EqualTo(a * b));
        Assert.That((char)(a_ / b_), Is.EqualTo(a / b));
        Assert.That((char)(b_ % a_), Is.EqualTo(b % a));
        
        Assert.That((char)++a_, Is.EqualTo(++a));
        Assert.That((char)--a_, Is.EqualTo(--a));

        Assert.That(a_ > b_, Is.False);
        Assert.That(a_ >= b_, Is.False);
        Assert.That(a_ == b_, Is.False);
        Assert.That(a_ <= b_, Is.True);
        Assert.That(a_ < b_, Is.True);
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