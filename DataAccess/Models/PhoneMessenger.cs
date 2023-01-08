namespace DataAccess.Models;

public class PhoneMessenger : Messenger
{
    public PhoneMessenger(
        Guid id,
        int messagesAmount,
        string number)
        : base(id, messagesAmount)
    {
        Number = number;
    }

    protected PhoneMessenger() { }
    public string Number { get; set; }
}