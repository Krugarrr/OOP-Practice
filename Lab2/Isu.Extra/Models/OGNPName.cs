using System.ComponentModel.DataAnnotations;

namespace Isu.Extra.Models;

public class OgnpName : IEquatable<OgnpName>
{
    public OgnpName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public string Name { get; }

    public bool Equals(OgnpName other)
        => other is not null
           && Name.Equals(other.Name);

    public override bool Equals(object obj)
    {
        if (obj is OgnpName name)
            return Equals(name);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}