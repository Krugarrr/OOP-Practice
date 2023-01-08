namespace DataAccess.Models;

public class Session
{
    public Session(int id, string login, string password)
    {
        Id = id;
        Login = login;
        Password = password;
    }
    
    protected Session() { }
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}