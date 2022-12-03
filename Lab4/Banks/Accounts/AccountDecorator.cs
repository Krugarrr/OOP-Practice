using System.Text.Json.Serialization.Metadata;
using Banks.BankEntity;
using Banks.ClientEntity;
using Banks.TransactionEntity;

namespace Banks.Accounts;

public abstract class AccountDecorator : IEquatable<AccountDecorator>
{
    protected AccountDecorator(Account account)
    {
        AccountWrap = account;
    }

    protected Account AccountWrap { get; private set; }

    public void SetAccount(Account account)
    {
        AccountWrap = account;
    }

    public int GetId() => AccountWrap.Id;
    public decimal GetBalance() => AccountWrap.Balance;
    public Client GetOwner() => AccountWrap.Owner;
    public void SusInvoke() => AccountWrap.ApproveClient();

    public virtual void AddMoney(decimal money)
    {
        AccountWrap.AddMoney(money);
    }

    public virtual void TakeMoney(decimal money)
    {
        AccountWrap.TakeMoney(money);
    }

    public virtual void TransferMoney(decimal money, int id, Bank bank)
    {
        AccountWrap.TransferMoney(money, id, bank);
    }

    public AbstractTransaction GetTransaction(int id)
    {
        return AccountWrap.TransactionHistory.FirstOrDefault(t => t.Id.Equals(id));
    }

    public virtual void CalculateInterest(int days) { }

    public void Cancel(int id)
    {
        GetTransaction(id).CancelTransactionTemplateMethod(this);
    }

    public bool Equals(AccountDecorator other)
        => other is not null
           && AccountWrap.Equals(other.AccountWrap);

    public override bool Equals(object obj)
    {
        if (obj is AccountDecorator account)
        {
            return Equals(account);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(AccountWrap);
}