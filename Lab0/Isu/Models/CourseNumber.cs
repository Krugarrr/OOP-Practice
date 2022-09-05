using Isu.Tools;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    private const int MinNumber = 0;
    private const int MaxNumber = 4;

    public CourseNumber(int number)
    {
        if (number is <= MinNumber or > MaxNumber)
        {
            throw new CourseNumberException($"{number} is invalid course number");
        }

        Number = number;
    }

    public int Number { get; }

    public bool Equals(CourseNumber? other)
        => other is not null
           && Number.Equals(other.Number);

    public override bool Equals(object? obj)
    {
        if (obj is CourseNumber number)
        {
            return Equals(number);
        }

        return false;
    }

    public override int GetHashCode()
        => Number.GetHashCode();
}
