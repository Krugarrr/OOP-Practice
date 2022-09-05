using Isu.Models;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(int id, string name, GroupName group, CourseNumber course)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(course);

        Id = id;
        Name = name;
        Group = group;
        Course = course;
    }

    public int Id { get; }
    public string Name { get; }
    public GroupName Group { get; private set; }
    public CourseNumber Course { get; }

    public bool Equals(Student? other)
        => other is not null
           && Id.Equals(other.Id)
           && Name.Equals(other.Name)
           && Group.Equals(other.Group)
           && Course.Equals(other.Course);

    public override bool Equals(object? obj)
    {
        if (obj is Student student)
        {
            return Equals(student);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Id, Name, Group, Course);

    internal void ChangeGroup(GroupName groupName)
    {
        Group = groupName;
    }
}