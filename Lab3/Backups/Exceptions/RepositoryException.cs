namespace Backups.Exceptions;

public class RepositoryException : BackupException
{
    public RepositoryException(string message)
        : base(message) { }

    public static RepositoryException RestorePointCreationException()
    {
        throw new RepositoryException("Could not create restore point, backup objects you wanted does not exist");
    }
}