using Shops.Models;

namespace Shops.Tools;

public class ShopEntityException : ShopException
{
    public ShopEntityException(string message)
        : base(message) { }

    public static ProductException ShopDoesNotExistError()
    {
        throw new ProductException("You're trying to find a non-existent store");
    }

    public static ShopEntityException WrongShopNameError()
    {
        throw new ShopEntityException("Your shop's name is empty or null");
    }

    public static ShopEntityException EmptySupplyError()
    {
        throw new ProductException($"Empty supply delivered");
    }
}