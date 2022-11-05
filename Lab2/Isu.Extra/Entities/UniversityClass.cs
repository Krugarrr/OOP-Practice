using System.ComponentModel.DataAnnotations;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class UniversityClass : IEquatable<UniversityClass>
{
    public UniversityClass(string name, Teacher teacher, ClassTime classTime, ClassAddress classAddress)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(teacher);
        ArgumentNullException.ThrowIfNull(classAddress);
        ArgumentNullException.ThrowIfNull(classTime);

        Name = name;
        Teacher = teacher;
        ClassTime = classTime;
        ClassAddress = classAddress;
    }

    public string Name { get; }

    public Teacher Teacher { get; }
    public ClassTime ClassTime { get; }
    public ClassAddress ClassAddress { get; }

    public bool Equals(UniversityClass other)
        => other is not null
           && Name.Equals(other.Name)
           && Teacher.Equals(other.Teacher)
           && ClassTime.Equals(other.ClassTime)
           && ClassAddress.Equals(other.ClassAddress);

    public override bool Equals(object obj)
    {
        if (obj is UniversityClass name)
            return Equals(name);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Teacher, ClassAddress, ClassTime);
    }
}
