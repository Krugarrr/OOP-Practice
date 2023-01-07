using Backups.Entities;

namespace Backups.Extra.Recover;

public interface IRecoverRestorePointStrategy
{
    void Execute(RestorePoint restorePoint);
}