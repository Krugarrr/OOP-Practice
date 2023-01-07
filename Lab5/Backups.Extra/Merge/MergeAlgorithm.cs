using Backups.Algorithm;
using Backups.Archiver;
using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;

namespace Backups.Extra.Merge;

public class MergeAlgorithm
{
    public MergeAlgorithm(
        IAlgorithmStrategy algorithmStrategy,
        IArchiver archiver,
        IRepository repository,
        string archivePath)
    {
        AlgorithmStrategy = algorithmStrategy;
        Archiver = archiver;
        Repository = repository;
        ArchivePath = archivePath;
    }

    public IAlgorithmStrategy AlgorithmStrategy { get; }
    public IArchiver Archiver { get; }

    public IRepository Repository { get; }
    public string ArchivePath { get; }

    public RestorePoint Merge(IReadOnlyList<RestorePoint> restorePoints)
    {
        IOrderedEnumerable<RestorePoint> sortedRestorePoints = restorePoints.OrderByDescending(rp => rp.Date);
        var backupObjects = new List<BackupObject>();
        var repoObjects = new List<IRepositoryObject>();
        foreach (RestorePoint rp in sortedRestorePoints)
        {
            foreach (BackupObject bo in rp.BackupObjects)
            {
                if (backupObjects.Contains(bo)) continue;
                backupObjects.Add(bo);
                IRepositoryObject roEqual = rp.Storage.GetObjects().First(ro => ro.Name == bo.GetName());
                repoObjects.Add(roEqual);
            }
        }

        IStorage storage = AlgorithmStrategy.Execute(repoObjects, Repository, Archiver, ArchivePath);
        return new RestorePoint(backupObjects, DateTime.Now, storage);
    }
}