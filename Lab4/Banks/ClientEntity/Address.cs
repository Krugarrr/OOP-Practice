namespace Banks.ClientEntity;

public class Address : IEquatable<Address>
{
    private const int MinimalHouseNumber = 0;

    public Address(string city, string street, int houseNumber)
    {
        ValidateAddress(city, street, houseNumber);
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public string City { get; }
    public string Street { get; }
    public int HouseNumber { get; }

    public bool Equals(Address other)
        => other is not null
           && City.Equals(other.City)
           && Street.Equals(other.Street)
           && HouseNumber.Equals(other.HouseNumber);

    public override bool Equals(object obj)
    {
        if (obj is Address address)
        {
            return Equals(address);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(City, Street, HouseNumber);
    }

    private void ValidateAddress(string city, string street, int houseNumber)
    {
        ArgumentNullException.ThrowIfNull(houseNumber);

        if (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street))
            throw new Exception();
        if (string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city))
            throw new Exception();
        if (houseNumber < MinimalHouseNumber)
            throw new Exception();
    }
}