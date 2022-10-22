using Shops.Models;

namespace Shops.Services.AddressBuilder;

public class AddressBuilder : IAddressBuilder
{
    private string _city;
    private string _street;
    private int _house;

    public AddressBuilder()
    {
        _city = null;
        _street = null;
        _house = 0;
    }

    public IAddressBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public IAddressBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }

    public IAddressBuilder WithHouse(int house)
    {
        _house = house;
        return this;
    }

    public Address Build()
    {
        return new Address(
            _city,
            _street,
            _house);
    }
}