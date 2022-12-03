using Banks.BankEntity;
using Banks.TransactionEntity;

namespace Banks.Accounts;

public class CreditAccount : AccountDecorator
{
    public CreditAccount(Account account)
        : base(account)
    {
    }

    public override void AddMoney(decimal money)
    {
        AccountWrap.AddMoney(money - TakeComission(money));
    }

    public override void TakeMoney(decimal money)
    {
        if (AccountWrap.Balance - money < AccountWrap.Configuration.CreditLimit)
            throw new Exception();
        AccountWrap.TakeMoney(TakeComission(money));
        AccountWrap.TakeMoney(money);
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
        if (AccountWrap.Balance - money < AccountWrap.Configuration.CreditLimit)
            throw new Exception();
        AccountWrap.TransferMoney(money - TakeComission(money), id, bank);
    }

    private decimal TakeComission(decimal money) => (money * AccountWrap.Configuration.CreditComission) / 100;
}