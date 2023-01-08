using Backups.Algorithm;
using Backups.Archiver;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;
using Newtonsoft.Json;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    [JsonProperty("backupObjects")]
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

    [JsonProperty("backup")]
    public Backup Backup { get; }

    [JsonProperty("repository")]
    public IRepository Repository { get; }

    [JsonProperty("algorithm")]
    public IAlgorithmStrategy Algorithm { get; }

    [JsonProperty("archiver")]
    public IArchiver Archiver { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupObjects.Remove(backupObject);
    }

    public void CreateRestorePoint(string archivePath)
    {
        if (string.IsNullOrWhiteSpace(archivePath))
            throw PathException.PathIsNullOrEmptyException();

        IReadOnlyList<IRepositoryObject> repositoryObjects = _backupObjects.Select(o => o.GetRepositoryObject()).ToList();
        IStorage storage = Algorithm.Execute(repositoryObjects, Repository, Archiver, archivePath);
        var restorePoint = new RestorePoint(_backupObjects, DateTime.Now, storage);
        Backup.AddRestorePoint(restorePoint);
    }
}