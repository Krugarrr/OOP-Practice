using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackupTask
{
    public void AddBackupObject(BackupObject backupObject);
    void RemoveBackupObject(BackupObject backupObject);
    public void CreateRestorePoint(string archivePath);
}