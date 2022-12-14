using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Algorithm;

public class SingleStorageAlgorithm : IAlgorithmStrategy
{
    public IStorage CreateZipArchive(
            IReadOnlyList<IRepositoryObject> objects,
            IRepository repository,
            IArchiver archiver,
            string archivePath,
            string archiveName)
        {
            return archiver.CreateZipStorage(archivePath, archiveName, objects, repository);
        }
}
