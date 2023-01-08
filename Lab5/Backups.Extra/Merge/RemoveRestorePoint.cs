using System.Globalization;
using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Merge;

public class RemoveRestorePoint : IEditRestorePointStrategy
{
    public void Execute(IBackup backup, IReadOnlyList<RestorePoint> restorePoints)
    {
        foreach (RestorePoint rp in restorePoints)
        {
            backup.RemoveRestorePoint(rp);
            rp.Storage.Suicide();
        }
    }
}