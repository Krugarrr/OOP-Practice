using Isu.Models;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(int id, string name, GroupName group)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(group);

        Id = id;
        Name = name;
        Group = group;
    }

    public int Id { get; }
    public string Name { get; }
    public GroupName Group { get; private set; }

    public bool Equals(Student? other)
        => other is not null
           && Id.Equals(other.Id)
           && Name.Equals(other.Name)
           && Group.Equals(other.Group);

    public override bool Equals(object? obj)
    {
        if (obj is Student student)
        {
            return Equals(student);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Id, Name, Group);

    internal void ChangeGroup(GroupName groupName)
    {
        Group = groupName;
    }
}