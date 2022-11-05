using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class OgnpStream : IEquatable<OgnpStream>
{
    private const int MaxStudentsAmount = 100;
    private readonly List<MegaFacultyStudent> _students;

    public OgnpStream(Schedule schedule, StreamName name)
    {
        ArgumentNullException.ThrowIfNull(schedule);
        ArgumentNullException.ThrowIfNull(name);

        Name = name;
        Schedule = schedule;
        _students = new List<MegaFacultyStudent>();
    }

    public StreamName Name { get; }
    public Schedule Schedule { get; }
    public IReadOnlyList<MegaFacultyStudent> Students => _students;

    public bool Equals(OgnpStream other)
        => other is not null
           && Name.Equals(other.Name)
           && Schedule.Equals(other.Schedule)
           && Students.Equals(other.Students);

    public override bool Equals(object obj)
    {
        if (obj is OgnpStream course)
            return Equals(course);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Schedule, Students);
    }

    internal MegaFacultyStudent AddStudent(MegaFacultyStudent student, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(student);
        _students.Add(student);
        if (Schedule.CompareClasses(schedule.ClassShedule))
            throw ScheduleException.ScheduleIntersectionException();

        student.AddOgnp(this);
        return student;
    }

    internal bool RemoveStudent(MegaFacultyStudent student)
    {
        if (!_students.Remove(student))
            throw OgnpException.StudentRemoveException();

        return student.RemoveOgnp(this);
    }

    internal bool IsFull() => Students.Count > MaxStudentsAmount;
}