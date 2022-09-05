namespace Isu.Tools;

public class StudentsCountException : IsuException
{
    public StudentsCountException() { }

    public StudentsCountException(string message)
        : base(message) { }

    public StudentsCountException(string message, Exception innerException)
        : base(message, innerException) { }
}