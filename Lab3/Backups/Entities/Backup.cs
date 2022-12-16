namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _backupHistory;
    public Backup()
    {
        _backupHistory = new List<RestorePoint>();
    }

    public IReadOnlyList<RestorePoint> BackupHistory => _backupHistory;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        _backupHistory.Add(restorePoint);
    }
}