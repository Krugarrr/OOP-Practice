using Backups.Entities;

namespace Backups.StorageEntity;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    public SplitStorage(List<IStorage> storages)
    {
        ArgumentNullException.ThrowIfNull(storages);
        _storages = storages;
    }
}