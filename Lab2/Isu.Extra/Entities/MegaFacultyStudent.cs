using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Entities;

public class MegaFacultyStudent : IEquatable<MegaFacultyStudent>
{
    private const int MaxOgnpCount = 2;
    private const int MinOgnpCount = 0;
    private readonly List<OgnpStream> _ognpStreams;
    public MegaFacultyStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        FacultyStudent = student;
        _ognpStreams = new List<OgnpStream>();
    }

    public Student FacultyStudent { get; }
    public IReadOnlyList<OgnpStream> OgnpStreams => _ognpStreams;

    public bool Equals(MegaFacultyStudent other)
        => other is not null
           && FacultyStudent.Equals(other.FacultyStudent)
           && OgnpStreams.Equals(other.OgnpStreams);

    public override bool Equals(object obj)
    {
        if (obj is MegaFacultyStudent course)
            return Equals(course);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FacultyStudent, OgnpStreams);
    }

    internal void AddOgnp(OgnpStream ognpStream)
    {
        ArgumentNullException.ThrowIfNull(ognpStream);
        if (_ognpStreams.Count >= MaxOgnpCount)
            throw OgnpException.OGNPLimitException();

        _ognpStreams.Add(ognpStream);
    }

    internal bool RemoveOgnp(OgnpStream ognpStream)
    {
        ArgumentNullException.ThrowIfNull(ognpStream);
        if (_ognpStreams.Count == MinOgnpCount)
            throw OgnpException.OGNPLimitException();

        return _ognpStreams.Remove(ognpStream);
    }
}