namespace Backups.RepositoryObjects.Interfaces;

public interface IDirectory : IRepositoryObject
{
    IReadOnlyCollection<IRepositoryObject> GetContents();
}