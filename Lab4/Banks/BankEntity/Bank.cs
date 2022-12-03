using System.Collections.Immutable;
using System.Reactive.Subjects;
using Banks.Accounts;
using Banks.ClientEntity;

namespace Banks.BankEntity;

public class Bank : IBank
{
    private readonly List<AccountDecorator> _accounts;
    private readonly List<Client> _clients;
    private int accountIdGenerator;
    public Bank(string name, BankConfiguration configuration)
    {
        _accounts = new List<AccountDecorator>();
        _clients = new List<Client>();
        Name = name;
        Configuration = configuration;
        accountIdGenerator = 0;
    }

    public string Name { get; }
    public BankConfiguration Configuration { get; private set; }
    public IReadOnlyList<AccountDecorator> Accounts => _accounts;

    public void CreateDebitAccount(Client client)
    {
        _clients.Add(client);
        _accounts.Add(new DebitAccount(new Account(accountIdGenerator++, Configuration, client)));
    }

    public void CreateDepositAccount(Client client)
    {
        _accounts.Add(new DepositAccount(new Account(accountIdGenerator++, Configuration, client)));
    }

    public void CreateCreditAccount(Client client)
    {
        _accounts.Add(new DepositAccount(new Account(accountIdGenerator++, Configuration, client)));
    }

    public void ChangeConfiguration(BankConfiguration newConfiguration)
    {
        Configuration = newConfiguration;
        foreach (IClient client in _clients)
        {
            client.Notify(Configuration, this);
        }
    }

    public void Fundraising(int days)
    {
        foreach (AccountDecorator account in _accounts)
        {
            account.CalculateInterest(days);
        }
    }

    public void ApproveClient(Client client)
    {
        Client findClient = _clients.FirstOrDefault(c => c == client)
                  ?? throw new Exception();
        if (findClient.Address is not null && findClient.Document != 0)
        {
            var accs = _accounts.Where(a => a.GetOwner() == findClient);
            foreach (var acc in accs)
            {
                acc.SusInvoke();
            }
        }
    }

    public AccountDecorator GetAccount(int id)
    {
        return _accounts.FirstOrDefault(a => a.GetId() == id);
    }
}