using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class ShoppingCartProduct : Product
{
    public ShoppingCartProduct(string name, int amount)
        : base(name)
    {
        ValidateCartProduct(amount);
        Amount = amount;
    }

    public int Amount { get; private set; }

    internal ShoppingCartProduct ChangeAmount(int number)
    {
        Amount += number;
        return this;
    }

    private void ValidateCartProduct(int amount)
    {
        ArgumentNullException.ThrowIfNull(amount);
        if (amount < 0)
            throw ProductException.ProductAmountError();
    }
}