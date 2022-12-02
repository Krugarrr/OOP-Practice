using Banks.Accounts;
using Banks.CentralBank;
using Banks.ClientEntity;

namespace Banks;

public class Bank : IBank
{
    private readonly List<AccountDecorator> _accounts;
    private readonly List<Client> _clients;

    public Bank(string name, BankConfiguration configuration)
    {
        _accounts = new List<AccountDecorator>();
        _clients = new List<Client>();
        Name = name;
        Configuration = configuration;
    }

    public string Name { get; }
    public BankConfiguration Configuration { get; }

    public AccountDecorator GetAccount(int id)
    {
        return _accounts.FirstOrDefault(a => a.GetId() == id);
    }
}