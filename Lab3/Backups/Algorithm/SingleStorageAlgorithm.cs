using Backups.Archiver;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Algorithm;

public class SingleStorageAlgorithm : IAlgorithmStrategy
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
                throw PathException.PathIsNullOrEmptyException();

            IReadOnlyList<IRepositoryObject> repositoryObjects = objects.Select(o => o.GetRepositoryObject()).ToList();
            return archiver.CreateZipStorage(archivePath, repositoryObjects, repository);
        }
}
