using Isu.Models;
using Isu.Tools;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int MaxStudents = 25;
    private const int MinStudents = 0;

    public Group(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
        Students = new List<Student>();
    }

    public GroupName Name { get; }
    public List<Student> Students { get; }
    public CourseNumber Course => Name.Course;
    public int Max => MaxStudents;
    public int Min => MinStudents;

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
        Validate();
        Students.Add(student);
        student.ChangeGroup(Name);
    }

    internal void RemoveStudent(int id)
    {
        Student? student = Students.Find(s => s.Id == id);
        Students.Remove(student!);
    }

    private void Validate()
    {
        if (Students.Count is < MinStudents or >= MaxStudents)
            throw new StudentsCountException($"The limit of students is exceeded, or there are none at all");
    }
}