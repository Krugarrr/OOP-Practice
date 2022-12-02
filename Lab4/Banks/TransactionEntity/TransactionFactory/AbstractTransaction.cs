using System.Transactions;

namespace Banks.TransactionEntity;

public abstract class AbstractTransacion
{
    public AbstractTransacion()
    {
        Time = DateTime.Now;

        // вынести в счёт
        Status = TransactionStatus.Committed;
    }

    public DateTime Time { get; }
    public int Id { get; }
    public decimal Sum { get; }
    public Bank TransferBank { get; }
    public int TransferAccountId { get; }
    public TransactionType Type { get; private protected set; }
    public TransactionStatus Status { get; private set; }

    private void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}