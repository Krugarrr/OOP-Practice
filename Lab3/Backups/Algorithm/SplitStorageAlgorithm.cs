using Backups.Archiver;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;

namespace Backups.Algorithm;

public class SplitStorageAlgorithm : IAlgorithmStrategy
{
    public IStorage Execute(
        IReadOnlyList<IRepositoryObject> repositoryObjects,
        IRepository repository,
        IArchiver archiver,
        string archivePath)
    {
        ArgumentNullException.ThrowIfNull(repositoryObjects);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(archiver);
        if (string.IsNullOrWhiteSpace(archivePath))
            throw PathException.PathIsNullOrEmptyException();

        var storages = repositoryObjects
                .Select(s => archiver
                .Archive(archivePath, repositoryObjects, repository))
                .ToList();
        return new SplitStorage(storages);
    }
}