using System.Transactions;

namespace Banks.TransactionEntity;

public class Transaction
{
    public Transaction(
        int id,
        decimal sum,
        TransactionType type)
    {
        Time = DateTime.Now;
        Id = id;
        Sum = sum;

        // вынести в счёт
        Type = type;
        Status = TransactionStatus.Committed;
    }

    public DateTime Time { get; }
    public int Id { get; }
    public decimal Sum { get; }
    public Bank TransferBank { get; }
    public int TransferAccountId { get; }
    public TransactionType Type { get; }
    public TransactionStatus Status { get; private set; }

    private void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}