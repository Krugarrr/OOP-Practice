using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class MarketProduct : Product
{
    private const decimal MinPrice = 0;
    private const int DecimalCompareValue = 0;

    public MarketProduct(string name, int amount, decimal price)
        : base(name)
    {
        ValidateMarketProduct(amount, price);
        Amount = amount;
        Price = price;
    }

    public int Amount { get; private set; }
    public decimal Price { get; private set; }

    public MarketProduct ChangePrice(decimal price)
    {
        return new MarketProduct(Name, Amount, price);
    }

    internal MarketProduct ChangeNumber(int number)
    {
        Amount += number;
        return this;
    }

    private void ValidateMarketProduct(int amount, decimal price)
    {
        ArgumentNullException.ThrowIfNull(amount);
        ArgumentNullException.ThrowIfNull(price);
        if (amount < 0)
            throw ProductException.ProductAmountError();
        if (decimal.Compare(price, MinPrice) < DecimalCompareValue)
            throw ProductException.ProductPriceError();
    }
}