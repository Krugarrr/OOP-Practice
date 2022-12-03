using System.Buffers;
using System.Runtime.Serialization;
using Banks.BankEntity;
using Banks.ClientEntity;
using Banks.TransactionEntity;
using Banks.TransactionEntity.TransactionFactory;

namespace Banks.Accounts;

public class Account : IEquatable<Account>
{
    private const decimal StartSum = 0;
    private readonly List<AbstractTransaction> _transactionHistory;
    private int idGenerator;

    public Account(
        int id,
        BankConfiguration configuration,
        Client owner)
    {
        Configuration = configuration;
        Owner = owner;
        Id = id;
        Balance = StartSum;

        idGenerator = 0;
        _transactionHistory = new List<AbstractTransaction>();
        TransactionFactory = new TransactionFactory();
        if (Owner.Address is null || Owner.Document == 0)
            Sussy = Suspection.Sus;
    }

    public int Id { get; }
    public decimal Balance { get; private set; }
    public BankConfiguration Configuration { get; }
    public Suspection Sussy { get; private set; }
    public ITransactionFactory TransactionFactory { get; }
    public IReadOnlyList<AbstractTransaction> TransactionHistory => _transactionHistory;

    public Client Owner { get; }
    public void AddMoney(decimal money)
    {
        Balance += money;
        AbstractTransaction transaction = TransactionFactory.CreateAddTransaction(idGenerator++, money);
        _transactionHistory.Add(transaction);
    }

    public void TakeMoney(decimal money)
    {
        TransactionLimitValidation(money);
        Balance -= money;
        AbstractTransaction transaction = TransactionFactory.CreateTakeTransaction(idGenerator++, money);
        _transactionHistory.Add(transaction);
    }

    public void TransferMoney(decimal money, int id, Bank bank)
    {
        TransactionLimitValidation(money);
        bank.GetAccount(id).AddMoney(money);
        AbstractTransaction transaction = TransactionFactory.CreateTransferTransaction(idGenerator++, money, id, bank);
        _transactionHistory.Add(transaction);
    }

    public void ApproveClient()
    {
        Sussy = Suspection.NotSus;
    }

    public AbstractTransaction GetTransaction(int id)
    {
        return _transactionHistory.FirstOrDefault(t => t.Id.Equals(id));
    }

    public bool Equals(Account other)
        => other is not null
           && Id.Equals(other.Id)
           && TransactionFactory.Equals(other.TransactionFactory)
           && Owner.Equals(other.Owner)
           && Configuration.Equals(other.Configuration)
           && Balance.Equals(other.Balance)
           && TransactionHistory.Equals(other.TransactionHistory);

    public override bool Equals(object obj)
    {
        if (obj is Account account)
        {
            return Equals(account);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Balance, Configuration, TransactionFactory, Id, Owner, TransactionHistory);

    private void TransactionLimitValidation(decimal money)
    {
        if (Sussy == Suspection.Sus)
        {
            if (money > Configuration.TransactionLimit)
                throw new Exception();
        }
    }
}