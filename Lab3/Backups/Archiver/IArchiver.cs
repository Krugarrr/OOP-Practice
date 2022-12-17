using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Archiver;

public interface IArchiver
{
    public IStorage CreateZipStorage(
        string archivePath,
        IReadOnlyList<IRepositoryObject> repoObjects,
        IRepository repository);
}