using Isu.Extra.Models;

namespace Isu.Extra.Services.AddressBuilder;

public interface IAddressBuilder
{
    public IAddressBuilder WithStreet(string street);
    public IAddressBuilder WithHousing(int housing);
    public IAddressBuilder WithClassroom(int classroom);
    public ClassAddress Build();
}