namespace DataAccess.Models;

public class Message
{
    public Message(DateTime date, string text, MessageStatus status, int id)
    {
        Date = date;
        Text = text;
        Status = status;
        Id = id;
    }
    protected Message() { }
    
    public DateTime Date { get; set; }
    public int Id { get; set; }
    public string Text { get; set; }
    public MessageStatus Status { get; set; }
}