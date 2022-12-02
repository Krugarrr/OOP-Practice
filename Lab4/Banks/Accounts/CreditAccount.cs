using Banks.TransactionEntity;
using Banks.TransactionEntity.TransactionChainOfResp;

namespace Banks.Accounts;

public class CreditAccount : AccountDecorator
{
    public CreditAccount(Account account)
        : base(account)
    {
    }
}