namespace Isu.Extra.Models;

public class ClassTime : IEquatable<ClassTime>
{
    private static readonly TimeOnly LessonTime = new TimeOnly(1, 50);

    public ClassTime(DayOfWeek dayOfWeek, TimeOnly classBegin)
    {
        ArgumentNullException.ThrowIfNull(dayOfWeek);
        ArgumentNullException.ThrowIfNull(classBegin);

        DayOfWeek = dayOfWeek;
        ClassBegin = classBegin;
    }

    public DayOfWeek DayOfWeek { get; }
    public TimeOnly ClassBegin { get; }

    public bool Equals(ClassTime other)
        => other is not null
           && DayOfWeek.Equals(other.DayOfWeek)
           && ClassBegin.Equals(other.ClassBegin);

    public override bool Equals(object obj)
    {
        if (obj is ClassTime group)
            return Equals(group);

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(DayOfWeek, ClassBegin);
}