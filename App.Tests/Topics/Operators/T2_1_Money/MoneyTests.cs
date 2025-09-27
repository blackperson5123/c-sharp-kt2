using App.Topics.Operators.T2_1_Money;
using NUnit.Framework;

namespace App.Tests.Topics.Operators.T2_1_Money;

public class MoneyTests
{
    [Test]
    public void Ctor_NullOrEmptyCurrency_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Money(null!, 0));
        Assert.Throws<ArgumentException>(() => new Money("", 0));
        Assert.Throws<ArgumentException>(() => new Money("   ", 0));
    }

    [Test]
    public void Equality_SameCurrency_CaseInsensitive_AndSameAmount()
    {
        var a = new Money("rub", 150);
        var b = new Money("RUB", 150);
        Assert.Multiple(() =>
        {
            Assert.That(a == b, Is.True);
        });
    }

    [Test]
    public void Inequality_DifferentCurrency_OrAmount()
    {
        var a = new Money("RUB", 100);
        var b = new Money("USD", 100);
        var c = new Money("RUB", 200);
        Assert.Multiple(() =>
        {
            Assert.That(a != b, Is.True);
            Assert.That(a != c, Is.True);
        });
    }

    [Test]
    public void Addition_SameCurrency_SumsAmounts()
    {
        var a = new Money("RUB", 120);
        var b = new Money("rub", 30);
        var c = a + b;
        Assert.Multiple(() =>
        {
            Assert.That(c.Currency, Is.EqualTo("RUB"));
            Assert.That(c.Amount, Is.EqualTo(150));
        });
    }

    [Test]
    public void Addition_DifferentCurrency_Throws()
    {
        var a = new Money("RUB", 100);
        var b = new Money("USD", 50);
        Assert.Throws<ArgumentException>(() => { var _ = a + b; });
    }

    [Test]
    public void Subtraction_SameCurrency_SubtractsAmounts()
    {
        var a = new Money("USD", 200);
        var b = new Money("usd", 50);
        var c = a - b;
        Assert.Multiple(() =>
        {
            Assert.That(c.Currency, Is.EqualTo("USD"));
            Assert.That(c.Amount, Is.EqualTo(150));
        });
    }

    [Test]
    public void Subtraction_DifferentCurrency_Throws()
    {
        var a = new Money("USD", 200);
        var b = new Money("RUB", 50);
        Assert.Throws<ArgumentException>(() => { var _ = a - b; });
    }

    [Test]
    public void Increment_Decrement_Operators_ChangeAmountByOne()
    {
        var a = new Money("RUB", 10);
        var preInc = ++a;
        Assert.Multiple(() =>
        {
            Assert.That(preInc.Amount, Is.EqualTo(11));
            Assert.That(a.Amount, Is.EqualTo(11));
        });
        var postIncOriginal = a++;
        Assert.Multiple(() =>
        {
            Assert.That(postIncOriginal.Amount, Is.EqualTo(11));
            Assert.That(a.Amount, Is.EqualTo(12));
        });
        var preDec = --a;
        Assert.Multiple(() =>
        {
            Assert.That(preDec.Amount, Is.EqualTo(11));
            Assert.That(a.Amount, Is.EqualTo(11));
        });
        var postDecOriginal = a--;
        Assert.Multiple(() =>
        {
            Assert.That(postDecOriginal.Amount, Is.EqualTo(11));
            Assert.That(a.Amount, Is.EqualTo(10));
        });
    }

    [Test]
    public void Multiplication_ByScalar_MultipliesAmount()
    {
        var a = new Money("EUR", 7);
        var c = a * 3;
        var d = 4 * a;
        Assert.Multiple(() =>
        {
            Assert.That(c.Currency, Is.EqualTo("EUR"));
            Assert.That(c.Amount, Is.EqualTo(21));
            Assert.That(d.Amount, Is.EqualTo(28));
        });
    }

    [Test]
    public void Division_ByScalar_DividesAmount()
    {
        var a = new Money("EUR", 20);
        var c = a / 3; // integer division expected
        Assert.Multiple(() =>
        {
            Assert.That(c.Currency, Is.EqualTo("EUR"));
            Assert.That(c.Amount, Is.EqualTo(6));
        });
    }

    [Test]
    public void Division_ByZero_Throws()
    {
        var a = new Money("EUR", 20);
        Assert.Throws<DivideByZeroException>(() => { var _ = a / 0; });
    }

    [Test]
    public void Modulo_ByScalar_ReturnsRemainderMoney()
    {
        var a = new Money("GBP", 23);
        var c = a % 5;
        Assert.Multiple(() =>
        {
            Assert.That(c.Currency, Is.EqualTo("GBP"));
            Assert.That(c.Amount, Is.EqualTo(3));
        });
    }

    [Test]
    public void Modulo_ByZero_Throws()
    {
        var a = new Money("GBP", 23);
        Assert.Throws<DivideByZeroException>(() => { var _ = a % 0; });
    }
}
