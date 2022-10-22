using Shops.Models;

namespace Shops.Services.AddressBuilder;

public interface IAddressBuilder
{
    public IAddressBuilder WithCity(string city);

    public IAddressBuilder WithStreet(string street);

    public IAddressBuilder WithHouse(int house);

    public Address Build();
}