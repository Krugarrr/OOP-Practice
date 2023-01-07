using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Interfaces;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;
using Newtonsoft.Json;

namespace Backups.Extra;

public class BackupTaskExtra : IBackupTask
{
    [JsonProperty("backupTask")]
    private BackupTask _backupTask;
    public BackupTaskExtra(
        BackupExtra backup,
        BackupTask backupTask)
    {
        Backup = backup;
        _backupTask = backupTask;
    }

    [JsonProperty("backup")]
    public BackupExtra Backup { get; }
    public void AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupTask.AddBackupObject(backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        _backupTask.RemoveBackupObject(backupObject);
    }

    public void CreateRestorePoint(string archivePath)
    {
        if (string.IsNullOrWhiteSpace(archivePath))
            throw PathException.PathIsNullOrEmptyException();

        _backupTask.CreateRestorePoint(archivePath);
    }
}