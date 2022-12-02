namespace Banks.TransactionEntity;

public interface ITransactionFactory
{
    public AbstractTransacion CreateAddTransaction(decimal money);
    public AbstractTransacion CreateTakeTransaction(decimal money);
    public AbstractTransacion CreateTransferTransaction(decimal money, int id, Bank bank);
}