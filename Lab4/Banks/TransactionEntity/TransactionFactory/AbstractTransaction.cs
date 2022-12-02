using System.Transactions;

namespace Banks.TransactionEntity;

public abstract class AbstractTransacion
{
    public AbstractTransacion(decimal money)
    {
        Time = DateTime.Now;
        Sum = money;

        // вынести в счёт
        Status = TransactionStatus.Committed;
    }

    public DateTime Time { get; }
    public int Id { get; }
    public decimal Sum { get; }
    public TransactionType Type { get; private protected set; }
    public TransactionStatus Status { get; private protected set; }
    protected abstract void ChangeStatus();
}