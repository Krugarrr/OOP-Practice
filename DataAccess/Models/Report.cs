namespace DataAccess.Models;

public class Report
{
    public Report(
        int messagesAmount, 
        TimeSpan messagesIntervalAmount)
    {
        Id = Guid.NewGuid();
        MessagesAmount = messagesAmount;
        MessengersUsages = new List<Messenger>();
        MessagesIntervalAmount = messagesIntervalAmount;
    }
    protected Report() { }
    public int MessagesAmount { get; set; }
    public Guid Id { get; set; }
    public virtual ICollection<Messenger> MessengersUsages { get; set; }
    public TimeSpan MessagesIntervalAmount { get; set; }
}