using System.Transactions;
using Banks.Accounts;

namespace Banks.TransactionEntity;

public class TakeTypeTransaction : AbstractTransaction
{
    public TakeTypeTransaction(decimal money)
        : base(money)
    {
        Type = TransactionType.Take;
    }

    protected override void CancelTakeMoney(AccountDecorator account)
    {
        account.AddMoney(Sum);
    }

    protected override void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}