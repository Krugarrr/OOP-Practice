namespace Banks;

public interface IBank
{
    public string Name { get; }
    public BankConfiguration Configuration { get; }
}