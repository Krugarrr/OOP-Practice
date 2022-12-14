using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    public FSRepository Repository { get; }
    public string StoragePath { get; }

    public IReadOnlyList<IRepositoryObject> GetRepositoryObjects()
    {
        throw new NotImplementedException();
    }
}