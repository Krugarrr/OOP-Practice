using System.Data.SqlTypes;
using Shops.Entities;
using Shops.Models;
using Shops.Tools;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private const int MinNumberOfAppropriatedShops = 0;
    private readonly List<Shop> _shops;

    public ShopManager(List<Shop> shops = null)
    {
        _shops = shops ?? new List<Shop>();
    }

    public IReadOnlyList<Shop> Shops => _shops;
    public Shop AddShop(Shop shop)
    {
        if (shop is null)
            throw new ShopException("Shop cannot be the null");

        if (_shops.FirstOrDefault(shopM => shopM.Id == shop.Id) is not null)
            throw new ShopException("Shop already registered");

        _shops.Add(shop);
        return _shops.Last();
    }

    public Shop FindShop(string name)
    {
        return _shops.FirstOrDefault(shop => shop.Name == name);
    }

    public Shop SupplyProducts(Shop shop, IReadOnlyList<MarketProduct> products)
    {
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(shop);
        Shop supply = FindShop(shop.Name);
        if (supply is null)
            throw ShopEntityException.EmptySupplyError();

        foreach (MarketProduct marketProduct in products)
            supply.AddProduct(marketProduct);
        return supply;
    }

    public Shop ChangePrice(string shopName, Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(product);
        ArgumentNullException.ThrowIfNull(newPrice);
        ArgumentNullException.ThrowIfNull(shopName);
        var shop = FindShop(shopName);
        if (shop is null)
            throw ShopEntityException.ShopDoesNotExistError();

        shop.ChangePrice(product.Name, newPrice);
        return shop;
    }

    public Customer AddProductsToCart(Customer customer, IReadOnlyList<ShoppingCartProduct> products)
    {
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(customer);

        foreach (ShoppingCartProduct shoppingCartProduct in products)
            customer.AddProduct(shoppingCartProduct);
        return customer;
    }

    public Shop FindShopWithCheapestProduct(IReadOnlyList<ShoppingCartProduct> products)
    {
        ArgumentNullException.ThrowIfNull(products);
        var shops = _shops.Where(s => s.ContainsAll(products)).ToList();
        return shops.Count == MinNumberOfAppropriatedShops ? null : shops.OrderByDescending(s => s.Receipt(products)).Last();
    }
}