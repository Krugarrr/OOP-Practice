using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public class AddTypeTransactionHandler : TransactionAbstractHandler
{
    public override object Handle(Account account, int id)
    {
        AbstractTransaction abstractTransaction = account.GetTransaction(id);
        if (abstractTransaction.Type == TransactionType.Add)
        {
            account.TakeMoney(abstractTransaction.Sum); // как будто это тоже костыль
            return " "; // костыль
        }

        return base.Handle(account, id);
    }
}