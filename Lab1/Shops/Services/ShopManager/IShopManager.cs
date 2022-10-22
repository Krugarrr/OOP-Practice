using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    public Shop AddShop(Shop shop);
    public Shop SupplyProducts(Shop shop, IReadOnlyList<MarketProduct> products);
    public Shop FindShop(string name);
    public Shop FindShopWithCheapestProduct(IReadOnlyList<ShoppingCartProduct> products);

    public Shop ChangePrice(string shopName, Product product, decimal newPrice);
    public Customer AddProductsToCart(Customer customer, IReadOnlyList<ShoppingCartProduct> products);
}