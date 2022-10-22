namespace Shops.Tools;

public class CustomerException : ShopException
{
    public CustomerException(string message)
        : base(message) { }

    public static CustomerException InvalidCutomerNameError()
    {
        throw new CustomerException("Customer can't have whitespace or nullable name");
    }

    public static CustomerException NegativeBalanceError()
    {
        throw new CustomerException("Customer can't have a negative balance (in my world)");
    }

    public static CustomerException NotEnoughMoneyError(decimal expected, decimal current)
    {
        throw new CustomerException($"Customer doesn't have enough money. \n Total cost: {expected} \n Balance: {current}");
    }
}