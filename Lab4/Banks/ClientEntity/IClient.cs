using Banks.BankEntity;

namespace Banks.ClientEntity;

public interface IClient
{
    public void Notify(BankConfiguration configuration, IBank bank);
}