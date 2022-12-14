using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public interface IArchiver
{
    public IStorage CreateZipStorage(
        string archivePath,
        string archiveName,
        IReadOnlyList<IRepositoryObject> repoObjects,
        IRepository repository);
}