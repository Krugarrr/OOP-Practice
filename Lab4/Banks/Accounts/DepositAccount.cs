using Banks.BankEntity;

namespace Banks.Accounts;

public class DepositAccount : AccountDecorator
{
    private const int DaysInMonth = 31;
    private int _dayLimit;
    private int _daysSpend;
    private decimal _interestSum;
    public DepositAccount(Account account)
        : base(account)
    {
        _dayLimit = AccountWrap.Configuration.DepositAccountTime;
        _daysSpend = 0;
    }

    public override void AddMoney(decimal money)
    {
        AccountWrap.AddMoney(money);
    }

    public override void TakeMoney(decimal money)
    {
        if (_daysSpend != 0)
            throw new Exception();
        if (AccountWrap.Balance < money)
            throw new Exception();
        AccountWrap.TakeMoney(money);
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
        if (_daysSpend != 0)
            throw new Exception();
        if (AccountWrap.Balance < money)
            throw new Exception();
        AccountWrap.TransferMoney(money, id, bank);
    }

    public override void CalculateInterest(int days)
    {
        if (days + _daysSpend > DaysInMonth)
        {
            int reminderAfter = _daysSpend + days - DaysInMonth;
            int reminderBefore = DaysInMonth - _daysSpend;
            _interestSum += reminderBefore * Calculate();
            AccountWrap.AddMoney(_interestSum);
            _interestSum = 0;
            _daysSpend = reminderAfter;
        }
        else
        {
            _interestSum += days * Calculate();
            _daysSpend += days;
        }
    }

    private decimal Calculate() => AccountWrap.Balance * AccountWrap
        .Configuration
        .DepositInterestRate
        .FindSuitRate(AccountWrap.Balance) / 100 / 365;
}