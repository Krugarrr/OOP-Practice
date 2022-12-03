using System.Transactions;
using Banks.Accounts;

namespace Banks.TransactionEntity;

public class AddTypeTransaction : AbstractTransaction
{
    public AddTypeTransaction(decimal money)
        : base(money)
    {
        Type = TransactionType.Add;
    }

    protected override void CancelAddMoney(AccountDecorator account)
    {
        account.TakeMoney(Sum);
    }

    protected override void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}