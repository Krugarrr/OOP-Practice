using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule : IEquatable<Schedule>
{
    public Schedule(IReadOnlyList<UniversityClass> schedule)
    {
        ArgumentNullException.ThrowIfNull(schedule);
        ClassShedule = schedule;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public IReadOnlyList<UniversityClass> ClassShedule { get; }

    public bool Equals(Schedule other)
        => other is not null
           && ClassShedule.Equals(other.ClassShedule);

    public override bool Equals(object obj)
    {
        if (obj is Schedule name)
            return Equals(name);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClassShedule);
    }

    internal bool CompareClasses(IReadOnlyList<UniversityClass> anotherClasses)
        => ClassShedule.All(c => anotherClasses.All(ac => ac.ClassTime.Equals(c.ClassTime)));

    public class ScheduleBuilder
    {
        private readonly List<UniversityClass> _lessons;

        public ScheduleBuilder()
        {
            _lessons = new List<UniversityClass>();
        }

        public ScheduleBuilder AddClass(UniversityClass lesson)
        {
            _lessons.Add(lesson);
            return this;
        }

        public Schedule Build() => new Schedule(_lessons);
    }
}