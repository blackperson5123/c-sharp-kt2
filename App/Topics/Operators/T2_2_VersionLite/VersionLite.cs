// Topic: Operators — T2.2 VersionLite (relational operators)
// Задача: реализовать сравнение версий по лексикографическому порядку: (Major, Minor, Patch).
// Требования:
// - Публичные только для чтения свойства: int Major, Minor, Patch; задать через конструктор.
// - Реализовать IComparable<VersionLite> и операторы <, >, <=, >=.
// - Сравнение: сначала Major, затем Minor, затем Patch.
// - Конструктор должен проверять, что значения не отрицательные; иначе ArgumentOutOfRangeException.

namespace App.Topics.Operators.T2_2_VersionLite;

public class VersionLite  : IComparable<VersionLite>
{
    public int Major { get; }
    public int Minor { get; }
    public int Patch { get; }

    public VersionLite(int major, int minor, int patch)
    {
        if (major < 0)
            throw new ArgumentOutOfRangeException(nameof(major), "Value cannot be negative");
        if (minor < 0)
            throw new ArgumentOutOfRangeException(nameof(minor), "Value cannot be negative");
        if (patch < 0)
            throw new ArgumentOutOfRangeException(nameof(patch), "Value cannot be negative");

        Major = major;
        Minor = minor;
        Patch = patch;
    }

    public int CompareTo(VersionLite other)
    {
        if (other == null)
            return 1;

        int result = Major.CompareTo(other.Major);
        if (result != 0) return result;

        result = Minor.CompareTo(other.Minor);
        if (result != 0) return result;

        return Patch.CompareTo(other.Patch);
    }

    public override bool Equals(object obj)
    {
        return obj is VersionLite other && Equals(other);
    }

    protected bool Equals(VersionLite other)
    {
        return Major == other.Major && Minor == other.Minor && Patch == other.Patch;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Major.GetHashCode();
            hash = hash * 31 + Minor.GetHashCode();
            hash = hash * 31 + Patch.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(VersionLite left, VersionLite right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return left.Equals(right);
    }

    public static bool operator !=(VersionLite left, VersionLite right)
    {
        return !(left == right);
    }

    public static bool operator <(VersionLite left, VersionLite right)
    {
        if (left is null)
            return right is object;
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(VersionLite left, VersionLite right)
    {
        if (left is null)
            return false;
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(VersionLite left, VersionLite right)
    {
        if (left is null)
            return true;
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(VersionLite left, VersionLite right)
    {
        if (left is null)
            return right is null;
        return left.CompareTo(right) >= 0;
    }
}