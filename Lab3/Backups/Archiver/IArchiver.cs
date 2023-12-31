using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;

namespace Backups.Archiver;

public interface IArchiver
{
    public IStorage Archive(
        string archivePath,
        IReadOnlyList<IRepositoryObject> repoObjects,
        IRepository repository);
}