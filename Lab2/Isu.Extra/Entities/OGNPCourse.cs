using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class OgnpCourse : IEquatable<OgnpCourse>
{
    private readonly List<OgnpStream> _streams;

    public OgnpCourse(OgnpName name, MegaFacultyName megaFacultyOwner)
    {
        Name = name;
        MegaFacultyOwner = megaFacultyOwner;
        _streams = new List<OgnpStream>();
    }

    public OgnpName Name { get; }
    public MegaFacultyName MegaFacultyOwner { get; }
    public IReadOnlyList<OgnpStream> Streams => _streams;

    public bool Equals(OgnpCourse other)
        => other is not null
           && Name.Equals(other.Name)
           && MegaFacultyOwner.Equals(other.MegaFacultyOwner)
           && Streams.Equals(other.Streams);

    public override bool Equals(object obj)
    {
        if (obj is OgnpCourse course)
            return Equals(course);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, MegaFacultyOwner, Streams);
    }

    internal OgnpStream AddStream(StreamName name, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(name);
        if (StreamExist(name))
            throw OgnpException.StreamDoesntExistException();

        var stream = new OgnpStream(schedule, name);
        _streams.Add(stream);
        return stream;
    }

    internal OgnpStream AddStudent(MegaFacultyStudent student, Schedule groupSchedule)
    {
        ArgumentNullException.ThrowIfNull(student);

        IEnumerable<OgnpStream> streams = Streams.Where(s => !s.IsFull());
        OgnpStream stream = streams.FirstOrDefault(s => !s.Schedule.CompareClasses(groupSchedule.ClassShedule))
                            ?? throw ScheduleException.ScheduleIntersectionException();
        return stream;
    }

    internal void RemoveStudent(MegaFacultyStudent student, OgnpStream stream)
    {
        stream.RemoveStudent(student);
    }

    private bool StreamExist(StreamName name)
        => _streams.Any(s => s.Name.Equals(name));
}