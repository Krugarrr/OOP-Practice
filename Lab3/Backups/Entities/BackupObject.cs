using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(IRepository repository, string objectPath)
    {
        ObjectPath = objectPath;
        Repository = repository;
    }

    public IRepository Repository { get; }
    public string ObjectPath { get; }

    public IRepositoryObject GetRepositoryObject()
    {
        return Repository.CreateRepositoryObject(this);
    }
}