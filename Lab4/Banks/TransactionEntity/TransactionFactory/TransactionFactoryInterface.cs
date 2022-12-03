using Banks.BankEntity;

namespace Banks.TransactionEntity;

public interface ITransactionFactory
{
    public AbstractTransaction CreateAddTransaction(int id, decimal money);
    public AbstractTransaction CreateTakeTransaction(int id, decimal money);
    public AbstractTransaction CreateTransferTransaction(int id, decimal money, int anotherId, Bank bank);
}