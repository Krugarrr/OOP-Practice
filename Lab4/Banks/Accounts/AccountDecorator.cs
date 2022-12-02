using System.Text.Json.Serialization.Metadata;

namespace Banks.Accounts;

public abstract class AccountDecorator : AbstractAccount
{
    protected AccountDecorator(Account account)
    {
        this.AccountWrap = account;
    }

    protected Account AccountWrap { get; set; }

    public void SetAccount(Account account)
    {
        this.AccountWrap = account;
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

    public override void Cancel()
    {
        AccountWrap.Cancel();
    }
}