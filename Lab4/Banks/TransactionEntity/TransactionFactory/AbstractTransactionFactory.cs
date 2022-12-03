namespace Banks.TransactionEntity;

public class TransactionFactory : ITransactionFactory
{
    public AbstractTransaction CreateAddTransaction(decimal money)
    {
        return new AddTypeTransaction(money);
    }

    public AbstractTransaction CreateTakeTransaction(decimal money)
    {
        return new TakeTypeTransaction(money);
    }

    public AbstractTransaction CreateTransferTransaction(decimal money, int id, Bank bank)
    {
        return new TransferTypeAbstractTransaction(money, bank, id);
    }
}