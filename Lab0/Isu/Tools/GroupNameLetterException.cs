namespace Isu.Tools;

public class GroupNameLetterException : IsuException
{
    public GroupNameLetterException() { }

    public GroupNameLetterException(string message)
        : base(message) { }

    public GroupNameLetterException(string message, Exception innerException)
        : base(message, innerException) { }
}