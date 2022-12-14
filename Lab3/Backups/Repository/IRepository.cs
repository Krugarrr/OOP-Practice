using Backups.Entities;
using Backups.RepositoryObjects.Interface;

namespace Backups.Repository;

public interface IRepository
{
    public IRepositoryObject CreateRepositoryObject(BackupObject backupObject);
    public Stream OpenFile(string path);
}
