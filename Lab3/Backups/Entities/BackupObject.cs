using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(FileSystemRepository repository, string objectPath)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrWhiteSpace(objectPath))
            throw new Exception();

        // string fullPath = $"{repository.RepositoryPath}{objectPath}";
        string fullPath = $"{objectPath}";
        ObjectPath = fullPath;
        Repository = repository;
    }

    public IRepository Repository { get; }
    public string ObjectPath { get; }

    public IRepositoryObject GetRepositoryObject() => Repository.CreateRepositoryObject(this);
}