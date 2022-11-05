using Isu.Extra.Models;

namespace Isu.Extra.Services.AddressBuilder;

public class AddressBuilder : IAddressBuilder
{
    private string _street;
    private int _housing;
    private int _classroom;

    public AddressBuilder()
    {
        _street = null;
        _housing = 0;
        _classroom = 0;
    }

    public IAddressBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }

    public IAddressBuilder WithHousing(int housing)
    {
        _housing = housing;
        return this;
    }

    public IAddressBuilder WithClassroom(int classroom)
    {
        _classroom = classroom;
        return this;
    }

    public ClassAddress Build()
    {
        return new ClassAddress(
            _street,
            _housing,
            _classroom);
    }
}