namespace Isu.Tools;

public class GroupNameLengthException : IsuException
{
    public GroupNameLengthException() { }

    public GroupNameLengthException(string message)
        : base(message) { }

    public GroupNameLengthException(string message, Exception innerException)
        : base(message, innerException) { }
}