using System.Text.Json.Serialization.Metadata;
using Banks.TransactionEntity;

namespace Banks.Accounts;

public abstract class AccountDecorator : AbstractAccount
{
    protected AccountDecorator(Account account)
    {
        AccountWrap = account;
    }

    protected Account AccountWrap { get; set; }

    public void SetAccount(Account account)
    {
        AccountWrap = account;
    }

    public int GetId() => AccountWrap.Id;

    public override void AddMoney(decimal money)
    {
        AccountWrap.AddMoney(money);
    }

    public override void TakeMoney(decimal money)
    {
        AccountWrap.TakeMoney(money);
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
        AccountWrap.TransferMoney(money, id, bank);
    }

    public AbstractTransaction GetTransaction(int id)
    {
        return AccountWrap.TransactionHistory.FirstOrDefault(t => t.Id.Equals(id));
    }

    public void Cancel(int id)
    {
        GetTransaction(id).CancelTransactionTemplateMethod(this);
    }
}