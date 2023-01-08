namespace DataAccess.Models;

public class Report
{
    public Report(
        int messagesAmount, 
        DateTime time)
    {
        Id = Guid.NewGuid();
        MessagesAmount = messagesAmount;
        MessengersUsages = new List<Messenger>();
        Time = time;
    }
    protected Report() { }
    public int MessagesAmount { get; set; }
    public Guid Id { get; set; }
    public virtual ICollection<Messenger> MessengersUsages { get; set; }
    public DateTime Time { get; set; }
}