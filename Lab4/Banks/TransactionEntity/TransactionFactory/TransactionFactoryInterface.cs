namespace Banks.TransactionEntity;

public interface ITransactionFactory
{
    public AbstractTransaction CreateAddTransaction(decimal money);
    public AbstractTransaction CreateTakeTransaction(decimal money);
    public AbstractTransaction CreateTransferTransaction(decimal money, int id, Bank bank);
}