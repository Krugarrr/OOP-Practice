namespace Banks.TransactionEntity;

public class TransactionFactory : ITransactionFactory
{
    public AbstractTransacion CreateAddTransaction(decimal money)
    {
        return new AddTypeTransaction(money);
    }

    public AbstractTransacion CreateTakeTransaction(decimal money)
    {
        return new TakeTypeTransaction(money);
    }

    public AbstractTransacion CreateTransferTransaction(decimal money, int id, Bank bank)
    {
        return new TransferTypeAbstractTransaction(money, bank, id);
    }
}