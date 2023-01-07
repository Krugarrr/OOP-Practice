using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Extra.Recover;

public class RecoverToDifferent : IRecoverRestorePointStrategy
{
    public RecoverToDifferent(FileSystemRepository repository)
    {
        Repository = repository;
    }

    public FileSystemRepository Repository { get; }

    public void Execute(RestorePoint restorePoint)
    {
        var visitor = new RecoverVisitor(Repository, string.Empty);
        foreach (BackupObject bo in restorePoint.BackupObjects)
        {
            IRepositoryObject killme = restorePoint.Storage.GetObjects().FirstOrDefault(ro => ro.Name == bo.GetName());
            killme.Accept(visitor);
        }
    }

    private string GetName(BackupObject bo)
    {
        string path = bo.ObjectPath;
        int index = path.LastIndexOf("\\");
        if (path.EndsWith("\\"))
        {
            path = path.Substring(0, path.Length - 1);
        }

        index = path.LastIndexOf("\\");
        index += index == 0 ? -1 : 0;
        return path.Substring(index + 1, path.Length - index - 1);
    }
}