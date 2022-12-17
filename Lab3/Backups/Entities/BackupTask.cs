using Backups.Algorithm;
using Backups.Archiver;
using Backups.Exceptions;
using Backups.Repository;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects;

    public BackupTask(
        IRepository repository,
        IArchiver archiver,
        List<BackupObject> backupObjects,
        IAlgorithmStrategy algorithm,
        Backup backup)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(algorithm);
        ArgumentNullException.ThrowIfNull(backup);

        _backupObjects = backupObjects;
        Algorithm = algorithm;
        Backup = backup;
        Repository = repository;
        Archiver = archiver;
    }

    public Backup Backup { get; }
    public IRepository Repository { get; }
    public IAlgorithmStrategy Algorithm { get; }
    public IArchiver Archiver { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupObjects.Add(backupObject);
    }

    public void CreateRestorePoint(string archivePath)
    {
        if (string.IsNullOrWhiteSpace(archivePath))
            throw PathException.PathIsNullOrEmptyException();

        IStorage storage = Algorithm.RunZipAlgorithm(_backupObjects, Repository, Archiver, archivePath);
        var restorePoint = new RestorePoint(_backupObjects, DateTime.Now, storage);
        Backup.AddRestorePoint(restorePoint);
    }
}