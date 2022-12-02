using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public class TransferTypeTransactionHandler : TransactionAbstractHandler
{
    public override object Handle(Account account, int id)
    {
        AbstractTransacion abstractTransacion = account.GetTransaction(id);
        if (abstractTransacion.Type == TransactionType.Transfer)
        {
            var transferTransaction = abstractTransacion as TransferTypeAbstractTransaction;
            account.AddMoney(abstractTransacion.Sum); // как будто это тоже костыль
            AbstractAccount anotherBankAccount = transferTransaction.TransferBank.GetAccount(abstractTransacion.Id);
            anotherBankAccount.TakeMoney(abstractTransacion.Sum);
            return " "; // костыль
        }

        return base.Handle(account, id);
    }
}