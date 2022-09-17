namespace Isu.Tools;

public class StudentExistException : IsuException
{
    public StudentExistException() { }

    public StudentExistException(string message)
        : base(message) { }

    public StudentExistException(string message, Exception innerException)
        : base(message, innerException) { }
}