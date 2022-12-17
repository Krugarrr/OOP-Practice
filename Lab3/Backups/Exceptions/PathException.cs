namespace Backups.Exceptions;

public class PathException : BackupException
{
    public PathException(string message)
        : base(message) { }

    public static PathException PathIsNullOrEmptyException()
    {
        throw new PathException("This path is null or empty");
    }
}