using Backups.Entities;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.StorageEntity;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    public SplitStorage(List<IStorage> storages)
    {
        ArgumentNullException.ThrowIfNull(storages);
        _storages = storages;
    }

    public IReadOnlyList<IRepositoryObject> GetObjects() => _storages.SelectMany(s => s.GetObjects()).ToList();
}