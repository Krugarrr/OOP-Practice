using System.Transactions;
using Banks.TransactionEntity.TransactionChainOfResp;

namespace Banks.Accounts;

public class DebitAccount : AccountDecorator
{
    public DebitAccount(Account account)
        : base(account)
    {
    }

    public override void AddMoney(decimal money)
    {
        // TransactionLimitValidation(money);
        base.AddMoney(money);
    }

    public override void TakeMoney(decimal money)
    {
        // TransactionLimitValidation(money);
        if (AccountWrap.Balance < money)
            throw new Exception();
        AccountWrap.AddMoney(money);
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
        TransactionLimitValidation(money);
        bank.GetAccount(id).AddMoney(money);
    }

    private void TransactionLimitValidation(decimal money)
    {
        if (money > AccountWrap.Configuration.TransactionLimit)
            throw new Exception();
    }
}