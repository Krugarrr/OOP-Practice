using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Algorithm;

public interface IAlgorithmStrategy
{
    IStorage CreateZipArchive(
        IReadOnlyList<IRepositoryObject> objects,
        IRepository repository,
        IArchiver archiver,
        string archivePath,
        string archiveName);
}