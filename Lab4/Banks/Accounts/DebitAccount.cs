using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;
using Banks.BankEntity;
using Banks.TransactionEntity;

namespace Banks.Accounts;

public class DebitAccount : AccountDecorator, IEquatable<DebitAccount>
{
    private const int DaysInMonth = 31;
    private int _daysSpend;
    private decimal _interestSum;
    public DebitAccount(Account account)
        : base(account)
    {
        _daysSpend = 0;
        _interestSum = 0;
    }

    public override void AddMoney(decimal money)
    {
        AccountWrap.AddMoney(money);
    }

    public override void TakeMoney(decimal money)
    {
        if (AccountWrap.Balance < money)
            throw new Exception();
        AccountWrap.TakeMoney(money);
    }

    public override void TransferMoney(decimal money, int id, Bank bank)
    {
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
            _daysSpend = reminderAfter;
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

    public bool Equals(DebitAccount other)
        => other is not null
           && AccountWrap.Equals(other.AccountWrap);

    public override bool Equals(object obj)
    {
        if (obj is DebitAccount account)
        {
            return Equals(account);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(AccountWrap);

    private void TransactionLimitValidation(decimal money)
    {
        if (money > AccountWrap.Configuration.TransactionLimit)
            throw new Exception();
    }

    private decimal Calculate() => AccountWrap.Balance * AccountWrap.Configuration.DebitInterestRate / 100 / 365;
}