using System.Security.Cryptography;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class Teacher : IEquatable<Teacher>
{
    public Teacher(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public string Name { get; }

    public bool Equals(Teacher other)
        => other is not null
           && Name.Equals(other.Name);

    public override bool Equals(object obj)
    {
        if (obj is Teacher name)
        {
            return Equals(name);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}