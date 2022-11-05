using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class MegaFacultyGroup : IEquatable<MegaFacultyGroup>
{
    public MegaFacultyGroup(Group facultyGroup, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(facultyGroup);
        ArgumentNullException.ThrowIfNull(schedule);
        FacultyGroup = facultyGroup;
        Schedule = schedule;
    }

    public Group FacultyGroup { get; }
    public Schedule Schedule { get; }
    public bool Equals(MegaFacultyGroup other)
        => other is not null
           && FacultyGroup.Equals(other.FacultyGroup)
           && Schedule.Equals(other.Schedule);

    public override bool Equals(object obj)
    {
        if (obj is MegaFacultyGroup course)
            return Equals(course);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FacultyGroup, Schedule);
    }
}