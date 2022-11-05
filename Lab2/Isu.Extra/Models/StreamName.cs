using System.ComponentModel.DataAnnotations;

namespace Isu.Extra.Models;

public class StreamName : IEquatable<StreamName>
{
    public StreamName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public string Name { get; }
    public bool Equals(StreamName other)
        => other is not null
           && Name.Equals(other.Name);

    public override bool Equals(object obj)
    {
        if (obj is StreamName name)
            return Equals(name);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}