using System.Transactions;
using Banks.Accounts;

namespace Banks.TransactionEntity.TransactionChainOfResp;

public abstract class TransactionAbstractHandler : ITransactionHandler
{
    private ITransactionHandler _nextHandler;

    public ITransactionHandler SetNext(ITransactionHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual object Handle(Account account, int id)
    {
        if (account.GetTransaction(id).Status == TransactionStatus.Aborted)
            throw new Exception();
        return _nextHandler is null ? _nextHandler.Handle(account, id) : null;
    }
}