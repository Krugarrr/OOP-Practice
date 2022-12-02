using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public class TakeTypeTransactionHandler : TransactionAbstractHandler
{
    public override object Handle(Account account, int id)
    {
        AbstractTransacion abstractTransacion = account.GetTransaction(id);
        if (abstractTransacion.Type == TransactionType.Take)
        {
            account.AddMoney(abstractTransacion.Sum); // как будто это тоже костыль
            return " "; // костыль
        }

        return base.Handle(account, id);
    }
}