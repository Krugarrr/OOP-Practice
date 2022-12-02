namespace Banks.Accounts;

public class DepositAccount : AccountDecorator
{
    public DepositAccount(Account account)
        : base(account)
    {
    }
}