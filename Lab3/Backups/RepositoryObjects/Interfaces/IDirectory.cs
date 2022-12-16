namespace Backups.RepositoryObjects.Interfaces;

public interface IDirectory : IRepositoryObject
{
    List<IRepositoryObject> InvokeStream();
}