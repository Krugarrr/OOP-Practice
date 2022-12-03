using Banks.Accounts;
using Banks.ClientEntity;

namespace Banks.BankEntity;

public interface IBank
{
    public void CreateDebitAccount(Client client);
    public void CreateDepositAccount(Client client);
    public void CreateCreditAccount(Client client);
    public void ChangeConfiguration(BankConfiguration newConfiguration);
    public void Fundraising(int days);
    public AccountDecorator GetAccount(int id);
}