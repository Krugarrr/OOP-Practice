using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public class TransferTypeTransactionHandler : TransactionAbstractHandler
{
    public override object Handle(Account account, int id)
    {
        Transaction transaction = account.GetTransaction(id);
        if (transaction.Type == TransactionType.Transfer)
        {
            account.AddMoney(transaction.Sum); // как будто это тоже костыль
            AbstractAccount anotherBankAccount = transaction.TransferBank.GetAccount(transaction.Id);
            anotherBankAccount.TakeMoney(transaction.Sum);
            return " "; // костыль
        }

        return base.Handle(account, id);
    }
}