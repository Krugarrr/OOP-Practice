using Backups.Entities;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Interfaces;

namespace Backups.Extra;

public class BackupExtra : IBackup
{
    public BackupExtra(
        Backup backup,
        IEditRestorePointStrategy restorePointEditor,
        ILogger logger,
        ILimitAlgorithm limitAlgorithmStrategy)
    {
        Backup = backup;
        RestorePointEditor = restorePointEditor;
        Logger = logger;
        LimitAlgorithm = limitAlgorithmStrategy;
    }

    public Backup Backup { get; }
    public IEditRestorePointStrategy RestorePointEditor { get; }
    public ILogger Logger { get; }
    public ILimitAlgorithm LimitAlgorithm { get; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        UpdateBackupHistory();
        Backup.AddRestorePoint(restorePoint);
        Logger.Log($"New restore point has just been added");
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        Backup.RemoveRestorePoint(restorePoint);
        Logger.Log($"Restore point of {restorePoint.Date} has been removed");
    }

    private void UpdateBackupHistory()
    {
        IReadOnlyList<RestorePoint> badPoints = LimitAlgorithm.Run(Backup.BackupHistory);
        RestorePointEditor.Execute(this, badPoints);
        Logger.Log("Backup history was updated");
    }
}