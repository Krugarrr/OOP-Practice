using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public class AddTypeTransactionHandler : TransactionAbstractHandler
{
    public override object Handle(Account account, int id)
    {
        Transaction transaction = account.GetTransaction(id);
        if (transaction.Type == TransactionType.Add)
        {
            account.TakeMoney(transaction.Sum); // как будто это тоже костыль
            return " "; // костыль
        }

        return base.Handle(account, id);
    }
}