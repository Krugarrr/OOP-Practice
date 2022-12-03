using System.Transactions;
using Banks.Accounts;
using Banks.BankEntity;

namespace Banks.TransactionEntity;

public class TransferTypeAbstractTransaction : AbstractTransaction
{
    public TransferTypeAbstractTransaction(
        int id,
        decimal money,
        Bank transferBank,
        int transferAccountId)
        : base(id, money)
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