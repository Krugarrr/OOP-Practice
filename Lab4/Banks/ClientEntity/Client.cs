namespace Banks.ClientEntity;

public class Client
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
    }

    public string Name { get; }
    public string Surname { get; }
    public Address Address { get; }
    public int Document { get; }
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