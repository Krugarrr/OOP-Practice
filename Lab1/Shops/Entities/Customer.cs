using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class Customer
{
    private List<ShoppingCartProduct> _productCart;
    public Customer(string name, decimal money)
    {
        ValidateCustomer(name);
        Name = name;
        Wallet = new Wallet(money);
        _productCart = new List<ShoppingCartProduct>();
    }

    public string Name { get; }
    public Wallet Wallet { get; }
    public IReadOnlyList<ShoppingCartProduct> ProductCart => _productCart;

    internal ShoppingCartProduct AddProduct(ShoppingCartProduct cartProduct)
    {
        ArgumentNullException.ThrowIfNull(cartProduct);
        ShoppingCartProduct product = FindProduct(cartProduct.Name);
        if (product is not null)
            return product.ChangeAmount(cartProduct.Amount);

        _productCart.Add(cartProduct);
        return cartProduct;
    }

    internal ShoppingCartProduct FindProduct(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _productCart.FirstOrDefault(p => p.Name == name);
    }

    private void ValidateCustomer(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw CustomerException.InvalidCutomerNameError();
    }
}