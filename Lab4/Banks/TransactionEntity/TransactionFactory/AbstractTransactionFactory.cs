namespace Banks.TransactionEntity;

public class TransactionFactory : ITransactionFactory
{
    public AbstractTransacion CreateAddTakeTransaction()
    {
        return new AddnTakeTypeAbstractTransacion();
    }

    public AbstractTransacion CreateTransferTransaction(int id, Bank bank)
    {
        return new TransferTypeAbstractTransacion(bank, id);
    }
}