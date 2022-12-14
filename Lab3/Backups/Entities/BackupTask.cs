using Backups.Algorithm;
using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects;

    public BackupTask(IRepository repository, IArchiver archiver)
    {
        _backupObjects = new List<BackupObject>();
        Repository = repository;
        Archiver = archiver;
    }

    public IRepository Repository { get; }
    public IAlgorithmStrategy Algorithm { get; }
    public IArchiver Archiver { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;

    public RestorePoint CreateRestorePoint(string archivePath, string archiveName)
    {
        IReadOnlyList<IRepositoryObject> repositoryObjects = _backupObjects.Select(o => o.GetRepositoryObject()).ToList();
        IStorage storage = Algorithm.CreateZipArchive(repositoryObjects, Repository, Archiver, archivePath, archiveName);
        var date = DateTime.Now;
        return new RestorePoint(BackupObjects, date, storage);
    }
}