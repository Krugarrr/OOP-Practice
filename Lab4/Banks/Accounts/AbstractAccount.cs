namespace Banks.Accounts;

public abstract class AbstractAccount
{
    public abstract void AddMoney(decimal money);
    public abstract void TakeMoney(decimal money);
    public abstract void TransferMoney(decimal money, int id, Bank bank);
    public abstract void Cancel();
}