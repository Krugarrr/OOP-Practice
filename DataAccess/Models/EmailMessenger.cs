namespace DataAccess.Models;

public class EmailMessenger : Messenger
{
    public EmailMessenger(
        Guid id,
        int messagesAmount,
        string address)
        : base(id, messagesAmount)
    {
        Address = address;
    }

    protected EmailMessenger() { }
    public string Address { get; set; }
}