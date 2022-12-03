using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public class TransferTypeTransactionHandler : TransactionAbstractHandler
{
    public override object Handle(Account account, int id)
    {
        AbstractTransaction abstractTransaction = account.GetTransaction(id);
        if (abstractTransaction.Type == TransactionType.Transfer)
        {
            var transferTransaction = abstractTransaction as TransferTypeAbstractTransaction;
            account.AddMoney(abstractTransaction.Sum); // как будто это тоже костыль
            AbstractAccount anotherBankAccount = transferTransaction.TransferBank.GetAccount(abstractTransaction.Id);
            anotherBankAccount.TakeMoney(abstractTransaction.Sum);
            return " "; // костыль
        }

        return base.Handle(account, id);
    }
}