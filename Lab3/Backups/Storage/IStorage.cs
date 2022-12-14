using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public interface IStorage
{
    FSRepository Repository { get; }
    string StoragePath { get; }

    // IReadOnlyList<IRepositoryObject> GetRepositoryObjects();
}