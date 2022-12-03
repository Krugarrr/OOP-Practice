using System.Transactions;
using Banks.Accounts;

namespace Banks.TransactionEntity;

public abstract class AbstractTransaction
{
    public AbstractTransaction(int id, decimal money)
    {
        Id = id;
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

    public void CancelTransactionTemplateMethod(AccountDecorator account)
    {
        CancelAddMoney(account);
        CancelTakeMoney(account);
        CancelTransferMoney(account);
    }

    protected virtual void CancelAddMoney(AccountDecorator account) { }
    protected virtual void CancelTakeMoney(AccountDecorator account) { }
    protected virtual void CancelTransferMoney(AccountDecorator account) { }
    protected abstract void ChangeStatus();
}