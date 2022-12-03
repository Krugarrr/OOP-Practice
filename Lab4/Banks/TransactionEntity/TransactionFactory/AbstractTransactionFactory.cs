using Banks.BankEntity;

namespace Banks.TransactionEntity.TransactionFactory;

public class TransactionFactory : ITransactionFactory
{
    public AbstractTransaction CreateAddTransaction(int id, decimal money)
    {
        return new AddTypeTransaction(id, money);
    }

    public AbstractTransaction CreateTakeTransaction(int id, decimal money)
    {
        return new TakeTypeTransaction(id, money);
    }

    public AbstractTransaction CreateTransferTransaction(int id, decimal money, int anotherId, Bank bank)
    {
        return new TransferTypeAbstractTransaction(id, money, bank, anotherId);
    }
}