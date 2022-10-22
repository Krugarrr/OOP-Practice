using Shops.Tools;

namespace Shops.Models;

public class Product
{
    public Product(string name)
    {
        ValidateProduct(name);
        Name = name;
    }

    public string Name { get; }

    private void ValidateProduct(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw ProductException.WrongProductNameError();
    }
}