namespace Isu.Extra.Models;

public class ClassAddress : IEquatable<ClassAddress>
{
    public ClassAddress(string street, int housing, int classroom)
    {
        ArgumentNullException.ThrowIfNull(street);
        ArgumentNullException.ThrowIfNull(housing);
        ArgumentNullException.ThrowIfNull(classroom);

        Street = street;
        Housing = housing;
        Classroom = classroom;
    }

    public string Street { get; }
    public int Housing { get; }
    public int Classroom { get; }

    public bool Equals(ClassAddress other)
        => other is not null
           && Street.Equals(other.Street)
           && Housing.Equals(other.Housing)
           && Classroom.Equals(other.Classroom);

    public override bool Equals(object obj)
    {
        if (obj is ClassAddress address)
            return Equals(address);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, Housing, Classroom);
    }
}