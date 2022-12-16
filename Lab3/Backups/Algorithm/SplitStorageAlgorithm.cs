using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;

namespace Backups.Algorithm;

public class SplitStorageAlgorithm : IAlgorithmStrategy
{
    public IStorage RunZipAlgorithm(
        IReadOnlyList<BackupObject> objects,
        IRepository repository,
        IArchiver archiver,
        string archivePath)
    {
        ArgumentNullException.ThrowIfNull(objects);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(archiver);
        if (string.IsNullOrWhiteSpace(archivePath))
            throw new Exception();

        IReadOnlyList<IRepositoryObject> repositoryObjects = objects.Select(o => o.GetRepositoryObject()).ToList();
        var storages = repositoryObjects
                .Select(s => archiver
                .CreateZipStorage(archivePath, repositoryObjects, repository))
                .ToList();
        return new SplitStorage(storages);
    }
}