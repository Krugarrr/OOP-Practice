using Isu.Models;
using Isu.Tools;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int MaxStudents = 25;
    private const int MinStudents = 0;
    private readonly List<Student> _students;
    public Group(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
        _students = new List<Student>();
    }

    public GroupName Name { get; }
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
    public CourseNumber Course => Name.Course;
    public int MaxGroupCapacity => MaxStudents;
    public int MinGroupCapacity => MinStudents;

    public bool Equals(Group? other)
        => other is not null
           && Students.Equals(other.Students)
           && Name.Equals(other.Name);

    public override bool Equals(object? obj)
    {
        if (obj is Group group)
        {
            return Equals(group);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Students, Name);

    internal void AddStudent(Student student)
    {
        ValidateCountOfStudents();
        _students.Add(student);
        student.ChangeGroup(Name);
    }

    internal void RemoveStudent(int id)
    {
        Student? student = _students.Find(s => s.Id == id);
        _students.Remove(student!);
    }

    private void ValidateCountOfStudents()
    {
        if (Students.Count is < MinStudents or >= MaxStudents)
            throw new StudentsCountException($"The limit of students is exceeded, or there are none at all");
    }
}