namespace Shops.Tools;

public class AddressException : ShopException
{
    private AddressException(string message)
        : base(message) { }

    public static AddressException WrongCityAddressError()
    {
        throw new AddressException("Your city line in address is empty or null");
    }

    public static AddressException WrongStreetAddressError()
    {
        throw new AddressException("Your street line in address is empty or null");
    }

    public static AddressException WrongHouseAddressError(int number)
    {
        throw new ProductException($"House number {number} can't exist");
    }
}