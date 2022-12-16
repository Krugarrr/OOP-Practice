namespace Backups.RepositoryObjects.Interfaces;

public interface IFile : IRepositoryObject
{
    Stream InvokeStream();
}