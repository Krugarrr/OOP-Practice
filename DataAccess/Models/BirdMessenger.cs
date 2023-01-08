namespace DataAccess.Models;

public class BirdMessenger : Messenger
{
    public BirdMessenger(
        Guid id,
        int messagesAmount, 
        decimal loadCapacity)
        : base(id, messagesAmount)
    {
        LoadCapacity = loadCapacity;
    }

    protected BirdMessenger(decimal loadCapacity) {
        LoadCapacity = loadCapacity;
    }
    public decimal LoadCapacity { get; set; }
}