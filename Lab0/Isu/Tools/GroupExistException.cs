namespace Isu.Tools;

public class GroupExistException : IsuException
{
    public GroupExistException() { }

    public GroupExistException(string message)
        : base(message) { }

    public GroupExistException(string message, Exception innerException)
        : base(message, innerException) { }
}