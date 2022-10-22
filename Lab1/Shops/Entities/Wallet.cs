using Shops.Tools;

namespace Shops.Entities;

public class Wallet
{
    private const decimal MinBalance = 0;
    private const int DecimalCompareValue = 0;

    public Wallet(decimal money)
    {
        Balance = money;
    }

    public decimal Balance { get; private set; }

    internal void DecreaseBalance(decimal money)
    {
        if (decimal.Compare(Balance, money) < DecimalCompareValue)
            throw CustomerException.NegativeBalanceError();
        Balance -= money;
    }

    private void ValidateWallet(decimal balance)
    {
        if (decimal.Compare(balance, MinBalance) <= DecimalCompareValue)
            throw CustomerException.NegativeBalanceError();
    }
}