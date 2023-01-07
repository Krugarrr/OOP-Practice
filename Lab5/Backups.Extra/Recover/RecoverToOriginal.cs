using Backups.Entities;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Extra.Recover;

public class RecoverToOriginal : IRecoverRestorePointStrategy
{
    public void Execute(RestorePoint restorePoint)
    {
        foreach (var bo in restorePoint.BackupObjects)
        {
            IRepositoryObject killme = restorePoint.Storage.GetObjects().FirstOrDefault(ro => ro.Name == bo.GetName());
            var visitor = new RecoverVisitor(bo.Repository, string.Empty);
            killme.Accept(visitor);
        }
    }
}