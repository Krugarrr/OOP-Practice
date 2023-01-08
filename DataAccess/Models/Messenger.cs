namespace DataAccess.Models;

public abstract class Messenger
{
    public Messenger(Guid id, int messagesAmount)
    {
        MessagesAmount = messagesAmount;
        Id = id;
        Messages = new List<Message>();
    }
    protected Messenger() { }
    public int MessagesAmount { get; set; }
    public Guid Id { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}