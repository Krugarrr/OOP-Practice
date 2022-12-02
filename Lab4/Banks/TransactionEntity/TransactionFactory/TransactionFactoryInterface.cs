namespace Banks.TransactionEntity;

public interface ITransactionFactory
{
    public AbstractTransacion CreateAddTakeTransaction();
    public AbstractTransacion CreateTransferTransaction(int id, Bank bank);
}