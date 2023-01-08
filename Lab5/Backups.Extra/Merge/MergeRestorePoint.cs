using System.Globalization;
using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Merge;

public class MergeRestorePoint : IEditRestorePointStrategy
{
    public MergeRestorePoint(MergeAlgorithm merger)
    {
        Merger = merger;
    }

    public MergeAlgorithm Merger { get; }
    public void Execute(IBackup backup, IReadOnlyList<RestorePoint> restorePoints)
    {
        backup.AddRestorePoint(Merger.Merge(restorePoints));
        foreach (RestorePoint rp in restorePoints)
        {
            backup.RemoveRestorePoint(rp);
            rp.Storage.Suicide();
        }
    }
}