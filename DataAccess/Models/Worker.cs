namespace DataAccess.Models;

public class Worker
{
    public Worker(
        string name, 
        string login, 
        string password)
    {
        Name = name;
        Login = login;
        Password = password;
        DungeonMasters = new List<Worker>();
        Slaves = new List<Worker>();
        Messengers = new List<Messenger>();
    }

    protected Worker() { }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Worker> DungeonMasters { get; set; }
    public virtual ICollection<Worker> Slaves { get; set; }
    public virtual ICollection<Messenger> Messengers { get; set; }
}