using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Shops.Services.AddressBuilder;
using Xunit;
namespace Shops.Test;

public class ShopsTest
{
    private ShopManager _shopManager = new ShopManager();
    private AddressBuilder _addressBuilder = new AddressBuilder();

    [Fact]
    public void AddProductToShop_ProductsCanBeBought()
    {
        Address addressIskander = _addressBuilder
            .WithCity("Санкт-Петербург")
            .WithStreet("Улица Академика Павлова")
            .WithHouse(228).Build();

        Shop hrustikShop = _shopManager.AddShop(new Shop("Магазин Хрустика", Guid.NewGuid(), addressIskander));
        var firstProduct = new MarketProduct("Конфета Коровкины слёзы", 50, 5);
        var secondProduct = new MarketProduct("Молоко \"Коровье горе\"", 10, 20);

        _shopManager.SupplyProducts(hrustikShop, new List<MarketProduct> { firstProduct, secondProduct });

        var customer = new Customer("Кудашев Искандер", 228322);
        var customerProduct = new ShoppingCartProduct("Конфета Коровкины слёзы", 9);
        var anotherCustomerProduct = new ShoppingCartProduct("Молоко \"Коровье горе\"", 5);

        _shopManager.AddProductsToCart(customer, new List<ShoppingCartProduct> { customerProduct, anotherCustomerProduct });

        customer = hrustikShop.Sell(customer);
        MarketProduct product = hrustikShop.FindProduct("Конфета Коровкины слёзы");
        Assert.Equal(41, product.Amount);
        Assert.Equal(228177, customer.Wallet.Balance);
    }

    [Fact]
    public void AddProductToShop_ChangePriceOfProduct()
    {
        Address addressForest = _addressBuilder
            .WithCity("Иркутск")
            .WithStreet("Улица Лесопильная")
            .WithHouse(10).Build();

        Shop shop = _shopManager.AddShop(new Shop("Магазин столярных изделий \"Трясу сосну\"", Guid.NewGuid(), addressForest));

        var product = new MarketProduct("Чурки", 100000, 1);
        _shopManager.SupplyProducts(shop, new List<MarketProduct> { product });
        MarketProduct expectedProduct = shop.FindProduct("Чурки");

        Assert.Equal(1, expectedProduct.Price);

        shop = _shopManager.ChangePrice(shop.Name, product, 0);
        MarketProduct changedProduct = shop.FindProduct("Чурки");
        Assert.Equal(0, changedProduct.Price);
    }

    [Fact]
    public void FindShopWithCheapestPrice_FailedToFindProducts()
    {
        Address firstGachiAddress = _addressBuilder
            .WithCity("Гачилэнд")
            .WithStreet("Улица Раздевалки")
            .WithHouse(3).Build();

        Address secondGachiAddress = _addressBuilder
            .WithCity("Гачилэнд")
            .WithStreet("Улица имени Билли Херингтона")
            .WithHouse(2).Build();

        Address thirdGachiAddress = _addressBuilder
            .WithCity("Гачилэнд")
            .WithStreet("Улица Лезерменов")
            .WithHouse(1).Build();

        Shop firstShop = _shopManager.AddShop(new Shop("СпортДанжнМастер", Guid.NewGuid(), firstGachiAddress));
        Shop secondShop = _shopManager.AddShop(new Shop("Рай", Guid.NewGuid(), secondGachiAddress));
        Shop thirdShop = _shopManager.AddShop(new Shop("Магазин одежды из кожи \"Артист\"", Guid.NewGuid(), thirdGachiAddress));

        var firstProduct = new MarketProduct("Плавки", 44, 100);
        var secondProduct = new MarketProduct("Нимб", 20, 1000000);
        var thirdProduct = new MarketProduct("Портупея", 14, 140);
        var fourthProduct = new MarketProduct("Кожаный ремень", 13, 50);
        var anotherProduct = new MarketProduct("Кожаный ремень", 14, 5);

        _shopManager.SupplyProducts(firstShop, new List<MarketProduct>() { firstProduct, fourthProduct });
        _shopManager.SupplyProducts(secondShop, new List<MarketProduct>() { secondProduct });
        _shopManager.SupplyProducts(thirdShop, new List<MarketProduct>() { thirdProduct, anotherProduct });

        var firstCustomerProduct = new ShoppingCartProduct("Плавки", 2);
        var secondCustomerProduct = new ShoppingCartProduct("Кожаный ремень", 1);
        var thirdCustomerProduct = new ShoppingCartProduct("Гель для душа", 20);

        Shop cheapestShop = _shopManager.FindShopWithCheapestProduct(new List<ShoppingCartProduct>
                                                            { firstCustomerProduct, secondCustomerProduct });
        Shop failedShop = _shopManager.FindShopWithCheapestProduct(new List<ShoppingCartProduct>
                                                    { firstCustomerProduct, thirdCustomerProduct });
        Assert.Equal(cheapestShop, firstShop);
        Assert.Null(failedShop);
    }

    [Fact]
    public void SellProductInShop_BalanceOfCustomerAndAmountOfProductChanged()
    {
        _shopManager = new ShopManager();
        Address address = _addressBuilder
            .WithCity("Пенза")
            .WithStreet("Улица Амогусов")
            .WithHouse(322).Build();

        Shop sussyShop = _shopManager.AddShop(new Shop("Космодром", Guid.NewGuid(), address));
        var firstProduct = new MarketProduct("Костюм космонавта", 22, 1500);
        var secondProduct = new MarketProduct("Нож susa", 1, 200);

        _shopManager.SupplyProducts(sussyShop, new List<MarketProduct> { firstProduct, secondProduct });
        var imposter = new Customer("Артём Андреев", 100000);

        var firstCustomerProduct = new ShoppingCartProduct("Костюм космонавта", 1);
        var secondCustomerProduct = new ShoppingCartProduct("Нож susa", 1);
        _shopManager.AddProductsToCart(imposter, new List<ShoppingCartProduct>() { firstCustomerProduct, secondCustomerProduct });

        Shop shop = _shopManager.FindShop("Космодром");
        Assert.Equal(22, shop.FindProduct("Костюм космонавта").Amount);

        imposter = shop.Sell(imposter);
        Shop changedShop = _shopManager.FindShop("Космодром");
        Assert.Equal(21, changedShop.FindProduct("Костюм космонавта").Amount);
        Assert.Equal(98300, imposter.Wallet.Balance);
    }
}