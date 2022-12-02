using Banks.TransactionEntity;
using Banks.TransactionEntity.TransactionChainOfResp;

namespace Banks.Accounts;

public class Account : AbstractAccount
{
    private static TransferTypeTransactionHandler _tranferHandler;
    private static AddTypeTransactionHandler _addHandler;
    private static TakeTypeTransactionHandler _takeHandler;
    private readonly List<AbstractTransacion> _transactionHistory;

    public Account(
        int id,
        decimal balance,
        BankConfiguration configuration,
        string owner)
    {
        Id = id;
        Balance = balance;
        Configuration = configuration;
        Owner = owner;

        _transactionHistory = new List<AbstractTransacion>();
        _addHandler = new AddTypeTransactionHandler();
        _takeHandler = new TakeTypeTransactionHandler();
        _tranferHandler = new TransferTypeTransactionHandler();
        _addHandler.SetNext(_takeHandler).SetNext(_tranferHandler);
    }

    public int Id { get; }
    public decimal Balance { get; private set; }
    public BankConfiguration Configuration { get; }
    public string Owner { get; } // пофиксить потом ибо стринга - хуета
    public override void AddMoney(decimal money)
    {
        Balance += money;

        // придумать логику добавления транзакций _transactionHistory.Add(new Transaction());
    }

    public override void TakeMoney(decimal money)
    {
        Balance -= money;
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
        bank.GetAccount(id).AddMoney(money);
    }

    public override void Cancel()
    {
        _addHandler.Handle(this, Id);
    }

    public AbstractTransacion GetTransaction(int id)
    {
        return _transactionHistory.FirstOrDefault(t => t.Id.Equals(id));
    }
}