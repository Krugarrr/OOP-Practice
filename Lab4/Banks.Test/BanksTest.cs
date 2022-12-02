using Banks.CentralBank;
using Banks.ClientEntity;
using Banks.ClientEntity.Interfaces;
using Xunit;
namespace Banks.Test;

public class BanksTest
{
    private IAddressBuilder _addressBuilder = new AddressBuilder();
    private TimeManager timeManager = new TimeManager();
    private ICentralBank centralBank;
    private Address address;

    public BanksTest()
    {
        centralBank = CentralBankSingleton.GetInstace(timeManager);
        address = _addressBuilder
            .WithStreet("Кронва")
            .WithCity("Kukuevo")
            .WithHouse(147)
            .Build();
    }
}