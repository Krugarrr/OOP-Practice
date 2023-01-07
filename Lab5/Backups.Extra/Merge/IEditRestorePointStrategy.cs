using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Merge;

public interface IEditRestorePointStrategy
{
    void Execute(IBackup backup, IReadOnlyList<RestorePoint> restorePoints);
}