using Backups.Archiver;
using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;

namespace Backups.Algorithm;

public interface IAlgorithmStrategy
{
    IStorage Execute(
        IReadOnlyList<IRepositoryObject> repositoryObjects,
        IRepository repository,
        IArchiver archiver,
        string archivePath);
}