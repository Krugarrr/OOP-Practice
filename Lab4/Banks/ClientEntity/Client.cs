using System.ComponentModel;
using System.Reactive.Subjects;
using Banks.BankEntity;

namespace Banks.ClientEntity;

public class Client : IClient, IEquatable<Client>
{
    public Client(
        string name,
        string surname,
        Address address,
        int document)
    {
        Name = name;
        Surname = surname;
        Address = address;
        Document = document;
        Sub = new Subject<int>();
    }

    public string Name { get; }
    public string Surname { get; }
    public Address Address { get; }
    public int Document { get; }
    public Subject<int> Sub { get; }

    public void Notify(BankConfiguration configuration, IBank bank)
    {
        Console.WriteLine($"Interest list in bank {bank} has changed:");
        Console.WriteLine($"Debit interest rate: {configuration.DebitInterestRate}");
        Console.WriteLine($"Credit comission: {configuration.CreditComission}");
        Console.WriteLine($"Credit limit: {configuration.CreditLimit}");
        Console.WriteLine($"Deposit account time: {configuration.DepositAccountTime}");
        Console.WriteLine($"Transaction limit: {configuration.TransactionLimit}");
        Console.WriteLine($"Deposit interest rates:");
        foreach (var dip in configuration.DepositInterestRate.DepositInterestPoints)
        {
            Console.WriteLine($"\tFrom {dip.MinSum} to {dip.MaxSum} deposit interest rate is: {dip.InterestRate}");
        }

        Console.WriteLine($"\tCommon deposit interest rate: {configuration.DepositInterestRate.MaxInterestRate}");
    }

    public bool Equals(Client other)
        => other is not null
           && Name.Equals(other.Name)
           && Surname.Equals(other.Surname)
           && Address.Equals(other.Address)
           && Document.Equals(other.Document);
    public override bool Equals(object obj)
    {
        if (obj is Client client)
        {
            return Equals(client);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Name, Surname, Address, Document);
}

public class ClientBuilder
{
    private string clientName;
    private string clientSurname;
    private Address clientAddress;
    private int clientDocument;

    public ClientBuilder()
    {
        clientName = null;
        clientSurname = null;
        clientAddress = null;
        clientDocument = 0;
    }

    public ClientBuilder WithName(string name)
    {
        clientName = name;
        return this;
    }

    public ClientBuilder WithSurname(string surname)
    {
        clientSurname = surname;
        return this;
    }

    public ClientBuilder WithAddress(Address address)
    {
        clientAddress = address;
        return this;
    }

    public ClientBuilder WithDocument(int document)
    {
        clientDocument = document;
        return this;
    }

    public Client Build()
    {
        return new Client(
            clientName,
            clientSurname,
            clientAddress,
            clientDocument);
    }
}