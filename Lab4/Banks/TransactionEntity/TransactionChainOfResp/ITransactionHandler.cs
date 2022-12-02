using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public interface ITransactionHandler
{
    ITransactionHandler SetNext(ITransactionHandler handler);
    object Handle(Account account, int id);
}