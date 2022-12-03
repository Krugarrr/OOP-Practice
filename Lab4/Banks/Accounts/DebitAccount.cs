using System.Runtime.InteropServices.ComTypes;
using System.Transactions;
using Banks.TransactionEntity;
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
        AbstractTransaction transaction = AccountWrap.TransactionFactory.CreateAddTransaction(money);
        transaction.CancelTransactionTemplateMethod(this);
    }

    public override void TakeMoney(decimal money)
    {
        // TransactionLimitValidation(money);
        if (AccountWrap.Balance < money)
            throw new Exception();
        AbstractTransaction transaction = AccountWrap.TransactionFactory.CreateTakeTransaction(money);
        transaction.CancelTransactionTemplateMethod(this);
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
        TransactionLimitValidation(money);
        AbstractTransaction transaction = AccountWrap.TransactionFactory.CreateTransferTransaction(money, id, bank);
        transaction.CancelTransactionTemplateMethod(this);
    }

    private void TransactionLimitValidation(decimal money)
    {
        if (money > AccountWrap.Configuration.TransactionLimit)
            throw new Exception();
    }
}