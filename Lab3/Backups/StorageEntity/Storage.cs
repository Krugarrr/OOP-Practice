using System.Runtime.CompilerServices;
using Backups.ArchiveEntities;
using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.StorageEntity;

public class Storage : IStorage
{
    private readonly List<IZipObject> _zipObjects;
    public Storage(IRepository repository, IReadOnlyList<IZipObject> zipObjects, string path)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(zipObjects);
        if (string.IsNullOrWhiteSpace(path))
            throw new Exception();

        Repository = repository as FileSystemRepository;
        _zipObjects = zipObjects as List<IZipObject>;
        Path = path;
    }

    public FileSystemRepository Repository { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects => _zipObjects;
    public string Path { get; }

    public IReadOnlyList<IRepositoryObject> GetObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (IZipObject obj in _zipObjects)
        {
            repositoryObjects.Add(obj.Convert());
        }

        return repositoryObjects;
    }
}