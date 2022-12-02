using System.Transactions;

namespace Banks.TransactionEntity;

public class TransferTypeAbstractTransaction : AbstractTransacion
{
    public TransferTypeAbstractTransaction(
        decimal money,
        Bank transferBank,
        int transferAccountId)
        : base(money)
    {
        Type = TransactionType.Transfer;
        TransferBank = transferBank;
        TransferAccountId = transferAccountId;
    }

    public Bank TransferBank { get; }
    public int TransferAccountId { get; }

    protected override void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}