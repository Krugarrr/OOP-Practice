using System.Diagnostics;
using Shops.Models;

namespace Shops.Tools;

public class ProductException : ShopException
{
    public ProductException(string message)
    : base(message) { }

    public static ProductException ProductAmountError()
    {
        throw new ProductException("Amount of product can't be less than zero");
    }

    public static ProductException WrongProductNameError()
    {
        throw new ProductException("Your product's name is empty or null");
    }

    public static ProductException ProductPriceError()
    {
        throw new ProductException("Price of product can't be less than zero");
    }

    public static ProductException ProductExistError(string name)
    {
        throw new ProductException($"Product doesn't exist in {name}");
    }

    public static ProductException ProductShoppingCartExistError(string name)
    {
        throw new ProductException($"Couldn't find all desired products in {name}");
    }
}