using System.ComponentModel.DataAnnotations;
using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class MegaFacultyName : IEquatable<MegaFacultyName>
{
    public MegaFacultyName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public string Name { get; }

    public bool Equals(MegaFacultyName other)
        => other is not null
           && Name.Equals(other.Name);

    public override bool Equals(object obj)
    {
        if (obj is MegaFacultyName name)
            return Equals(name);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}