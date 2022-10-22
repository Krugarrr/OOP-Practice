using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class Shop
{
    private readonly List<MarketProduct> _products;

    public Shop(string name, Guid id, Address address)
    {
        ValidateShop(name, address);
        _products = new List<MarketProduct>();
        Name = name;
        Id = id;
        Address = address;
    }

    public string Name { get; }
    public Guid Id { get; }
    public Address Address { get; }

    public IReadOnlyCollection<MarketProduct> Products => _products;

    public Customer Sell(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer.ProductCart);
        ArgumentNullException.ThrowIfNull(customer);
        if (!ContainsAll(customer.ProductCart))
            throw ProductException.ProductShoppingCartExistError(Name);

        decimal receipt = Receipt(customer.ProductCart);
        if (receipt > customer.Wallet.Balance)
            throw CustomerException.NotEnoughMoneyError(receipt, customer.Wallet.Balance);

        customer.Wallet.DecreaseBalance(receipt);

        foreach (ShoppingCartProduct product in customer.ProductCart)
            FindProduct(product.Name).ChangeNumber(-product.Amount);

        return customer;
    }

    public MarketProduct FindProduct(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _products.FirstOrDefault(p => p.Name == name);
    }

    internal MarketProduct AddProduct(MarketProduct marketProduct)
    {
        ArgumentNullException.ThrowIfNull(marketProduct);

        MarketProduct product = FindProduct(marketProduct.Name);
        if (product is not null)
            return product.ChangeNumber(marketProduct.Amount);

        _products.Add(marketProduct);
        return marketProduct;
    }

    internal void ChangePrice(string name, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(newPrice);

        MarketProduct product = _products.FirstOrDefault(p => p.Name == name)
                                ?? throw ProductException.ProductExistError(name);

        _products.Remove(product);
        _products.Add(product.ChangePrice(newPrice));
    }

    internal bool ContainsAll(IReadOnlyList<ShoppingCartProduct> products)
        => products.All(product => _products.Any(p => p.Name == product.Name
                                                      && p.Amount >= product.Amount));

    internal decimal Receipt(IReadOnlyList<ShoppingCartProduct> products)
    {
        IEnumerable<MarketProduct> cart = _products.Where(p => products.Any(product => p.Name == product.Name));
        return cart.Sum(s => s.Price * products.First(p => p.Name == s.Name).Amount);
    }

    private void ValidateShop(string name, Address address)
    {
        ArgumentNullException.ThrowIfNull(address);
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw ShopEntityException.WrongShopNameError();
    }
}