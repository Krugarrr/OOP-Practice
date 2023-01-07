using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    public void AddRestorePoint(RestorePoint restorePoint);
    public void RemoveRestorePoint(RestorePoint restorePoint);
}