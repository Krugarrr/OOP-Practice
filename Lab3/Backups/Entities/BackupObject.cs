using Backups.Exceptions;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Newtonsoft.Json;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(FileSystemRepository repository, string objectPath)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrWhiteSpace(objectPath))
            throw PathException.PathIsNullOrEmptyException();

        string fullPath = $"{objectPath}";
        ObjectPath = fullPath;
        Repository = repository;
    }

    [JsonProperty("repository")]
    public IRepository Repository { get; }

    [JsonProperty("objectPath")]
    public string ObjectPath { get; }

    public IRepositoryObject GetRepositoryObject() => Repository.CreateRepositoryObject(this);
    public string GetName()
    {
        string path = ObjectPath;
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