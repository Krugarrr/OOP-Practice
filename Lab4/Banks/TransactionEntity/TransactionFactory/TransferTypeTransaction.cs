using System.Transactions;
using Banks.Accounts;

namespace Banks.TransactionEntity;

public class TransferTypeAbstractTransaction : AbstractTransaction
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

    protected override void CancelTransferMoney(AccountDecorator account)
    {
        account.AddMoney(Sum);
        TransferBank.GetAccount(TransferAccountId).TakeMoney(Sum);
    }

    protected override void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}