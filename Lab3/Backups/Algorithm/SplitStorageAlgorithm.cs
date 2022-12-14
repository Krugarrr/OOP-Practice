using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Algorithm;

public class SplitStorageAlgorithm : IAlgorithmStrategy
{
    public IStorage CreateZipArchive(
        IReadOnlyList<IRepositoryObject> objects,
        IRepository repository,
        IArchiver archiver,
        string archivePath,
        string archiveName)
    {
        return new SplitStorage(objects
                .Select(s => archiver
                .CreateZipStorage(archivePath, archiveName, objects, repository))
                .ToList());
    }
}