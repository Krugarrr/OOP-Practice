using System.Transactions;

namespace Banks.TransactionEntity;

public class TakeTypeTransaction : AbstractTransacion
{
    public TakeTypeTransaction(decimal money)
        : base(money)
    {
        Type = TransactionType.Take;
    }

    protected override void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}