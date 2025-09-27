// Topic: Operators — T2.1 Money (==, !=, Equals/GetHashCode)
// Задача: реализовать value object для денег.
// Требования:
// - Поля: string Currency (например, "RUB", "USD"), long Amount (в минимальных единицах: копейки/центы).
// - Конструктор Money(string currency, long amount) — должен проверять currency на null/пустую строку.
// - Реализовать IEquatable<Money>, переопределить Equals(object), GetHashCode, операторы == и !=.
// - Деньги равны только если совпадают и валюта, и сумма.
// Подсказка: подумайте о нормализации регистра Currency (например, ToUpperInvariant) — оговорено в тестах.

namespace App.Topics.Operators.T2_1_Money;

public struct Money : IEquatable<Money>
{
    public string Currency { get; }
    public long Amount { get; }

    public Money(string currency, long amount)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("0", nameof(currency));
        Currency = currency.ToUpperInvariant();
        Amount = amount;
    }
    public bool Equals(Money other)
    {
        return Currency == other.Currency && Amount == other.Amount;
    }

    public override bool Equals(object obj)
    {
        return obj is Money other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Currency.GetHashCode();
            hash = hash * 31 + Amount.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(Money left, Money right) => left.Equals(right);
    public static bool operator !=(Money left, Money right) => !left.Equals(right);

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new ArgumentException("нельзя, разные значения");
        return new Money(left.Currency, left.Amount + right.Amount);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new ArgumentException("нельзя, разные значения");
        return new Money(left.Currency, left.Amount - right.Amount);
    }
    public static Money operator *(Money money, long scalar) => new Money(money.Currency, money.Amount * scalar);
    public static Money operator *(long scalar, Money money) => new Money(money.Currency, money.Amount * scalar);

    public static Money operator /(Money money, long scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException();
        return new Money(money.Currency, money.Amount / scalar);
    }
    public static Money operator %(Money money, long scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException();
        return new Money(money.Currency, money.Amount % scalar);
    }

    public static Money operator ++(Money money) =>
        new Money(money.Currency, money.Amount + 1);

    public static Money operator --(Money money) =>
        new Money(money.Currency, money.Amount - 1);
}