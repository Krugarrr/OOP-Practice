using Backups.Entities;
using Backups.Repository;

namespace Backups.Algorithm;

public interface IAlgorithmStrategy
{
    IStorage RunZipAlgorithm(
        IReadOnlyList<BackupObject> objects,
        IRepository repository,
        IArchiver archiver,
        string archivePath);
}